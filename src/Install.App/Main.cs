using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Install.App;

public partial class Main : Form
{
    public Main()
    {
        InitializeComponent();
        Load += Main_Load;
    }

    private async void Main_Load(object? sender, EventArgs e)
    {
        foreach (var item in NetworkInterface.GetAllNetworkInterfaces().OrderBy(n => n.Description))
        {
            cbNetworkInterfaces.Items.Add(item.Description);
        }

        // IpAddress
        if (string.IsNullOrEmpty(Properties.Settings.Default.IpAddress))
        {
            return;
        }

        tbxIpAddress.Text = Properties.Settings.Default.IpAddress;

        // Gateway
        if (string.IsNullOrEmpty(Properties.Settings.Default.Gateway))
        {
            return;
        }

        tbxGateway.Text = Properties.Settings.Default.Gateway;

        // IpPublicAddress
        ckbUseIpPublic.Checked = Properties.Settings.Default.UseIpPublic;
        if (ckbUseIpPublic.Checked)
        {
            lblIpPublicAddress.Visible = true;
            tbxIpPublicAddress.Visible = true;
            tbxIpPublicAddress.Text = Properties.Settings.Default.IpPublicAddress;
        }
        else
        {
            lblIpPublicAddress.Visible = false;
            tbxIpPublicAddress.Visible = false;
            tbxIpPublicAddress.Text = string.Empty;
        }

        // NetworkInterface
        if (string.IsNullOrEmpty(Properties.Settings.Default.NetworkInterface))
        {
            return;
        }

        cbNetworkInterfaces.DropDownStyle = ComboBoxStyle.DropDownList;
        cbNetworkInterfaces.SelectedIndex = cbNetworkInterfaces.FindStringExact(Properties.Settings.Default.NetworkInterface);
        if (cbNetworkInterfaces.SelectedIndex < 0)
        {
            cbNetworkInterfaces.SelectedIndex = 0;
        }

        // Ram
        if (Properties.Settings.Default.Ram <= 7)
        {
            return;
        }

        nudRam.Value = Properties.Settings.Default.Ram;

        // Cpu
        if (Properties.Settings.Default.Cpu <= 5)
        {
            return;
        }

        nudCpus.Value = Properties.Settings.Default.Cpu;

        if (Properties.Settings.Default.MaxDiskSize <= 49)
        {
            return;
        }

        // MaxDiskSize
        tbxMaxDiskSize.Text = Properties.Settings.Default.MaxDiskSize.ToString();

        if (string.IsNullOrEmpty(Properties.Settings.Default.DataDirectory))
        {
            return;
        }

        tbxDataDirectory.Text = Properties.Settings.Default.DataDirectory;

        await StartAsync();
    }

    private void Main_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Return)
        {
            btnStart.PerformClick();
        }
    }

    private static IEnumerable<string> GetLocalIpAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        return host.AddressList.Where(n => n.AddressFamily == AddressFamily.InterNetwork).Select(n => n.ToString());
    }

    private async void btnStart_Click(object sender, EventArgs e)
    {
        var ipAddress = tbxIpAddress.Text.TrimStart().TrimEnd();
        if (GetLocalIpAddress().Contains(ipAddress))
        {
            MessageBox.Show("Trùng địa chỉ IP", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (string.IsNullOrWhiteSpace(ipAddress))
        {
            MessageBox.Show("Yêu cầu nhập địa chỉ IP", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var gateway = tbxGateway.Text.TrimStart().TrimEnd();
        if (string.IsNullOrWhiteSpace(gateway))
        {
            MessageBox.Show("Yêu cầu nhập gateway", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var useIpPublic = ckbUseIpPublic.Checked;
        var ipPublic = tbxIpPublicAddress.Text.TrimStart().TrimEnd();
        if (useIpPublic && string.IsNullOrWhiteSpace(ipPublic))
        {
            MessageBox.Show("Yêu cầu ip public", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var networkInterface = cbNetworkInterfaces.Text.TrimStart().TrimEnd();
        if (string.IsNullOrWhiteSpace(networkInterface))
        {
            MessageBox.Show("Yêu cầu nhập tên card mạng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var ram = (int)nudRam.Value;
        if (ram <= 7)
        {
            MessageBox.Show("Yêu cầu tối thiểu 8GB ram", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var cpu = (int)nudCpus.Value;
        if (cpu <= 5)
        {
            MessageBox.Show("Yêu cầu tối thiểu 6 Cores", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var maxDiskSize = int.Parse(tbxMaxDiskSize.Text);
        if (maxDiskSize <= 49)
        {
            MessageBox.Show("Yêu cầu giới hạn bộ nhớ tối thiểu 50GB", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var dataDirectory = tbxDataDirectory.Text;
        if (string.IsNullOrWhiteSpace(dataDirectory))
        {
            MessageBox.Show("Yêu cầu nhập đường dẫn dữ liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Properties.Settings.Default.IpAddress = ipAddress;
        Properties.Settings.Default.Gateway = gateway;
        Properties.Settings.Default.UseIpPublic = useIpPublic;
        Properties.Settings.Default.IpPublicAddress = ipPublic;
        Properties.Settings.Default.NetworkInterface = cbNetworkInterfaces.Text;
        Properties.Settings.Default.Ram = ram;
        Properties.Settings.Default.Cpu = cpu;
        Properties.Settings.Default.MaxDiskSize = maxDiskSize;
        Properties.Settings.Default.DataDirectory = dataDirectory;
        Properties.Settings.Default.Save();

        rtbLogs.Clear();
        Cursor = Cursors.WaitCursor;
        await StartAsync();
        Cursor = Cursors.Default;
    }

    private async Task StartAsync()
    {
        WriteLog("Bắt đầu cài đặt");

        await InitVagrantFileAsync();
        await InitEnvironmentAsync();
        await InstallVagrantPluginAsync();

        await RunVagrantUpAsync();
        WriteLog("Cài đặt thành công");
    }

    private async Task InitVagrantFileAsync()
    {
        EnsureExistDirectory($"{Properties.Settings.Default.DataDirectory}/minio");
        EnsureExistDirectory($"{Properties.Settings.Default.DataDirectory}/db-backups");

        await Task.Run(() =>
        {
            try
            {
                var content = new StringBuilder();
                content.Append("Vagrant.configure(\"2\") do |config|");
                content.AppendLine("  config.vm.box = \"ubuntu/focal64\"");
                content.AppendLine($"  config.disksize.size = '{int.Parse(Properties.Settings.Default.MaxDiskSize.ToString())}GB'");
                content.AppendLine("  config.vm.provider \"virtualbox\" do |vb|");
                content.AppendLine("    vb.name = \"ubuntu\"");
                content.AppendLine($"    vb.memory = \"{int.Parse(Properties.Settings.Default.Ram.ToString()) * 1024}\"");
                content.AppendLine($"    vb.cpus = {Properties.Settings.Default.Cpu}");
                content.AppendLine("  end");
                content.AppendLine(
                    $"  config.vm.network \"public_network\", ip: \"{Properties.Settings.Default.IpAddress}\", bridge: \"{Properties.Settings.Default.NetworkInterface}\"");
                content.AppendLine("  config.vm.provision \"shell\", inline: <<-SHELL");
                content.AppendLine($"    sudo ip route add default via {Properties.Settings.Default.Gateway}");
                content.AppendLine("  SHELL");
                content.AppendLine(
                    $"  config.vm.synced_folder \"{Properties.Settings.Default.DataDirectory}/minio\", \"/srv/psi8/minio\", mount_options: [\"dmode=775\", \"fmode=664\"]");
                content.AppendLine(
                    $"  config.vm.synced_folder \"{Properties.Settings.Default.DataDirectory}/db-backups\", \"/srv/psi8/postgresql/backups\", mount_options: [\"dmode=775\", \"fmode=664\"]");

                content.AppendLine(Properties.Settings.Default.UseIpPublic
                    ? "  config.vm.provision :shell, path: \"core/bootstrap.sh\", run: 'always'"
                    : "  config.vm.provision :shell, path: \"core/bootstrap.local.sh\", run: 'always'");

                content.AppendLine("end");
                File.WriteAllText("Vagrantfile", content.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        });
    }

    private void EnsureExistDirectory(string directory)
    {
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            WriteLog($"Đã tạo thư mục: {directory}");
        }
        else
        {
            WriteLog($"Thư mục đã tồn tại: {directory}");
        }
    }

    private static async Task InitEnvironmentAsync()
    {
        await Task.Run(() =>
        {
            try
            {
                var content = new StringBuilder();
                content.AppendLine($"IP_ADDRESS={Properties.Settings.Default.IpAddress}");
                content.AppendLine("OAUTH=http://${IP_ADDRESS}:3000");
                content.AppendLine("API_GATEWAY=http://${IP_ADDRESS}:5000");
                content.AppendLine("WEB_URL=http://${IP_ADDRESS}:2000");
                content.AppendLine("RABBITMQ_STOMP_ENDPOINT=${IP_ADDRESS}:15674");
                content.AppendLine("RABBITMQ_SSL=false");
                content.AppendLine("RABBITMQ_USERNAME=rabbitmq");
                content.AppendLine("RABBITMQ_PASSWORD=Pass1234!");

                if (Properties.Settings.Default.UseIpPublic && !string.IsNullOrEmpty(Properties.Settings.Default.IpPublicAddress))
                {
                    content.AppendLine($"IP_PUBLIC_ADDRESS={Properties.Settings.Default.IpPublicAddress}");
                    content.AppendLine("OAUTH_PUBLIC=http://${IP_PUBLIC_ADDRESS}:3001");
                    content.AppendLine("API_GATEWAY_PUBLIC=http://${IP_PUBLIC_ADDRESS}:5001");
                    content.AppendLine("WEB_PUBLIC_URL=http://${IP_PUBLIC_ADDRESS}:2001");
                    content.AppendLine("RABBITMQ_STOMP_PUBLIC_ENDPOINT=${IP_ADDRESS}:15676");
                }

                var file = new FileInfo("core/.env");
                file.Directory?.Create(); // If the directory already exists, this method does nothing.
                File.WriteAllText(file.FullName, content.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        });
    }

    private async Task InstallVagrantPluginAsync()
    {
        await Task.Run(() =>
        {
            try
            {
                var process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c vagrant plugin install vagrant-disksize";
                process.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

                process.OutputDataReceived += (s, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        rtbLogs.Invoke(() => { WriteLog(args.Data); });
                    }
                };
                process.ErrorDataReceived += (s, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        rtbLogs.Invoke(() => { WriteLog(args.Data); });
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
            }
            catch (Exception)
            {
                // ignored
            }
        });
    }

    private async Task RunVagrantUpAsync()
    {
        await Task.Run(() =>
        {
            var process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c vagrant up";
            process.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;

            process.OutputDataReceived += (s, args) =>
            {
                if (!string.IsNullOrEmpty(args.Data))
                {
                    rtbLogs.Invoke(() => { WriteLog(args.Data); });
                }
            };
            process.ErrorDataReceived += (s, args) =>
            {
                if (!string.IsNullOrEmpty(args.Data))
                {
                    rtbLogs.Invoke(() => { WriteLog(args.Data); });
                }
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        });
    }

    private void WriteLog(string content)
    {
        rtbLogs.AppendText($"[{DateTime.Now:HH:m:s}]: {content}\r\n");
    }

    public void rtbLogs_TextChanged(object sender, EventArgs e)
    {
        // set the current caret position to the end
        rtbLogs.SelectionStart = rtbLogs.Text.Length;

        // scroll it automatically
        rtbLogs.ScrollToCaret();
    }

    private void btnDataDirectory_Click(object sender, EventArgs e)
    {
        using var fbd = new FolderBrowserDialog();
        var result = fbd.ShowDialog();
        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
        {
            tbxDataDirectory.Text = fbd.SelectedPath.Replace("\\", "/");
        }
    }

    private void ckbUseIpPublic_CheckedChanged(object sender, EventArgs e)
    {
        if (ckbUseIpPublic.Checked)
        {
            lblIpPublicAddress.Visible = true;
            tbxIpPublicAddress.Visible = true;
        }
        else
        {
            lblIpPublicAddress.Visible = false;
            tbxIpPublicAddress.Visible = false;
            tbxIpPublicAddress.Text = string.Empty;
        }
    }
}