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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonClass
{
    /// <summary>
    /// ログ出力用のクラス
    /// </summary>
    public class LibLog
    {
        /// <summary>
        /// エラーメッセージの最初に付ける文言
        /// </summary>
        public string strError = "[[Error]] ";

        /// <summary>
        /// 警告メッセージの最初に付ける文言
        /// </summary>
        public string strWarning = "[[Warning]] ";

        string newLine = System.Environment.NewLine;

        TextBox textBox;

        bool dateTimeFlag = false;

        /// <summary>
        /// LibLogを初期化します。
        /// </summary>
        /// <param name="textBox">ログを表示するテキストボックス</param>
        /// <param name="dateTimeFlag">true:ログに日時を含める   false:含めない（デフォルト）</param>
        public void Init(TextBox textBox, bool dateTimeFlag = false)
        { 
            this.textBox = textBox;
            this.dateTimeFlag = dateTimeFlag;
        }

        /// <summary>
        /// ログを表示するテキストボックスの内容をクリアします。
        /// </summary>
        public void Clear() 
        {
            textBox.Clear();
        }

        /// <summary>
        /// メッセージをログに出力します。改行も出力します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        public void WriteLine(string message)
        {
            if (message == null)
            {
                textBox.AppendText("" + newLine);
            }
            else
            {
                Write(message + newLine);
            }
        }

        /// <summary>
        /// エラーメッセージをログに出力します。改行も出力します。
        /// </summary>
        /// <param name="message">エラーメッセージ</param>
        public void WriteLineError(string message)
        {
            if (message == null)
            {
                textBox.AppendText("" + newLine);
            }
            else
            {
                Write(strError + message + newLine);
            }
        }

        /// <summary>
        /// 警告メッセージをログに出力します。改行も出力します。
        /// </summary>
        /// <param name="message">警告メッセージ</param>
        public void WriteLineWarning(string message)
        {
            if (message == null)
            {
                textBox.AppendText("" + newLine);
            }
            else
            {
                Write(strWarning + message + newLine);
            }
        }

        /// <summary>
        /// メッセージをログに出力します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        public void Write(string message)
        {
            string mes;

            if (dateTimeFlag)
            {
                DateTime dTime = DateTime.Now;

                if (message == "")
                {
                    mes = "";
                }
                else
                {
                    mes = "" + dTime + "> " + message;
                }
            }
            else
            {
                mes = message;
            }

            textBox.AppendText(mes);
        }
    }
}
