/*
    The MIT License

    Copyright(c) 2024 OrgaiCom

    以下に定める条件に従い、本ソフトウェアおよび関連文書のファイル（以下「ソフトウェア」）
    の複製を取得するすべての人に対し、ソフトウェアを無制限に扱うことを無償で許可します。
    これには、ソフトウェアの複製を使用、複写、変更、結合、掲載、頒布、サブライセンス、
    および/または販売する権利、およびソフトウェアを提供する相手に同じことを許可する権利も
    無制限に含まれます。

    上記の著作権表示および本許諾表示を、ソフトウェアのすべての複製または重要な部分に
    記載するものとします。

    ソフトウェアは「現状のまま」で、明示であるか暗黙であるかを問わず、何らの保証もなく
    提供されます。ここでいう保証とは、商品性、特定の目的への適合性、および権利非侵害に
    ついての保証も含みますが、それに限定されるものではありません。 作者または著作権者は、
    契約行為、不法行為、またはそれ以外であろうと、ソフトウェアに起因または関連し、
    あるいはソフトウェアの使用またはその他の扱いによって生じる一切の請求、損害、
    その他の義務について何らの責任も負わないものとします。
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonClass
{
    /// <summary>
    /// OS関連のクラス（シャットダウンやWifi等）
    /// </summary>
    public class LibOs
    {
        /// <summary>
        /// コマンドプロンプトで指定されたコマンドを実行します。
        /// </summary>
        /// <param name="command">コマンド</param>
        public void ExecCmd(string command)
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            Process proc = new Process();
            proc.StartInfo = psi;
            proc.Start();

            proc.StandardInput.WriteLine(command);
            proc.StandardInput.Close();
            proc.WaitForExit();
        }

        /// <summary>
        /// 指定されたSSIDの Wi-Fiを接続します。
        /// </summary>
        /// <param name="ssid">SSID</param>
        public void ConnectWifi(string ssid)
        {
            ExecCmd("netsh.exe wlan connect name=\"" + ssid + "\"");
        }

        /// <summary>
        /// 現在接続中のWi-Fiを切断します。
        /// </summary>
        public void DisconnectWifi()
        {
            ExecCmd("netsh.exe wlan disconnect");
        }

        /// <summary>
        /// 確認のメッセージボックスを出した後、Windowsをシャットダウンします。
        /// </summary>
        /// <returns>bool | true:成功   false:失敗</returns>
        public bool Shutdown()
        {
            bool success = true;

            DialogResult dResult;

            dResult = MessageBox.Show("PCをシャットダウンします。", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            if (dResult == DialogResult.OK)
            {
                ExecCmd("shutdown.exe /s /t 0");
            }
            else
            {
                return false;
            }

            return success;
        }

        /// <summary>
        /// 確認のメッセージボックスを出した後、Windowsを再起動します。
        /// </summary>
        /// <returns>bool | true:成功   false:失敗</returns>
        public bool Restart()
        {
            bool success = true;

            DialogResult dResult;

            dResult = MessageBox.Show("PCを再起動します。", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            if (dResult == DialogResult.OK)
            {
                ExecCmd("shutdown.exe /r /t 0");
            }
            else
            {
                return false;
            }

            return success;
        }

        /// <summary>
        /// 確認のメッセージボックスを出した後、Windowsをサインアウトします。
        /// </summary>
        /// <returns>bool | true:成功   false:失敗</returns>
        public bool SignOut()
        {
            bool success = true;

            DialogResult dResult;

            dResult = MessageBox.Show("OSをサインアウトします。", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            if (dResult == DialogResult.OK)
            {
                ExecCmd("logoff.exe");
            }
            else
            {
                return false;
            }

            return success;
        }
    }
}
