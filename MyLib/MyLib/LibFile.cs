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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace CommonClass
{
    /// <summary>
    /// ファイルやディレクトリ（フォルダ）関連のクラス
    /// </summary>
    public class LibFile
    {
        /// <summary>
        /// フォルダを再帰的にコピーするための関数
        /// </summary>
        /// <param name="sourceDirName">コピー元のフォルダのパス</param>
        /// <param name="destDirName">コピー先のフォルダのパス</param>
        /// <param name="libLog">LibLogクラスのインスタンス。指定するとファイルコピーごとにログを出す。（デフォルト：null）</param>
        /// <param name="copySubDirs">true:再帰的にサブフォルダもコピーする（デフォルト）   false:しない</param>
        /// <returns>bool | true:成功   false:失敗</returns>
        public bool CopyDirectory(string sourceDirName, string destDirName, LibLog libLog = null, bool copySubDirs = true)
        {
            bool success = true;

            // コピー先のディレクトリがなければ作成
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // コピー元のディレクトリ内のファイルをコピー
            foreach (string file in Directory.GetFiles(sourceDirName))
            {
                success = FileCopyNewer(file, Path.Combine(destDirName, Path.GetFileName(file)), libLog);

                if(success == false)
                {
                    return false;
                }
            }

            // サブディレクトリが存在する場合は、それらもコピー
            if (copySubDirs)
            {
                // コピー元のディレクトリ内のディレクトリに対して、再帰的に呼び出す
                foreach (string dir in Directory.GetDirectories(sourceDirName))
                {
                    success = CopyDirectory(dir, Path.Combine(destDirName, Path.GetFileName(dir)), libLog);

                    if(success == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// ファイルをコピーします。
        /// ※ただし、コピー元が新しい場合のみコピーします。
        /// </summary>
        /// <param name="srcFilePath">コピー元ファイルのパス</param>
        /// <param name="dstFilePath">コピー先ファイルのパス</param>
        /// <param name="libLog">LibLogクラスのインスタンス。指定するとファイルコピーごとにログを出す。（デフォルト：null）</param>
        /// <returns>true:成功   false:失敗</returns>
        public bool FileCopyNewer(string srcFilePath, string dstFilePath, LibLog libLog = null)
        {
            bool result;
            DateTime dTimeSrc;
            DateTime dTimeDst;

            try
            {
                // コピー元ファイルが存在するかどうかを確認します。
                if (!System.IO.File.Exists(srcFilePath))
                {
                    if (libLog != null)
                    {
                        libLog.WriteLineError("ファイル（" + srcFilePath + "）は存在しません。");
                    }

                    return false;
                }

                dTimeSrc = File.GetLastWriteTime(srcFilePath);
                dTimeDst = File.GetLastWriteTime(dstFilePath);

                // コピー先ファイルが存在しない場合、またはコピー元ファイルが新しい場合、ファイルをコピーします。
                if (!File.Exists(dstFilePath))
                {
                    result = FileCopyWithTimeStamp(srcFilePath, dstFilePath, libLog);

                    return result;
                }
                else if (dTimeSrc > dTimeDst)
                {
                    result = FileCopyWithTimeStamp(srcFilePath, dstFilePath, libLog);

                    return result;
                }
                else if (dTimeSrc == dTimeDst)
                {
                    return true;
                }
                else if (dTimeSrc < dTimeDst)
                {
                    // コピー元ファイルがコピー先ファイルより古い場合、falseを返します。
                    if (libLog != null)
                    {
                        libLog.WriteLineError("ファイル（" + srcFilePath + "）より ファイル（" + dstFilePath + "）が新しいため コピー失敗しました。");
                    }

                    return false;
                }
            }
            catch
            {
            }

            // エラーが発生した場合は、falseを返します。
            if (libLog != null)
            {
                libLog.WriteLineError("ファイル（" + srcFilePath + "）をファイル（" + dstFilePath + "）にコピー失敗しました。");
            }

            return false;
        }

        /// <summary>
        /// ファイルコピーを行います。タイムスタンプもコピーします。
        /// </summary>
        /// <param name="srcFilePath">コピー元のファイルのパス</param>
        /// <param name="dstFilePath">コピー先のファイルのパス</param>
        /// <param name="libLog">LibLogクラスのインスタンス。指定するとファイルコピーごとにログを出す。（デフォルト：null）</param>
        /// <returns>bool | true:成功   false:失敗</returns>
        public bool FileCopyWithTimeStamp(string srcFilePath, string dstFilePath, LibLog libLog = null)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(srcFilePath);

                // USBフラッシュにコピーしたら、なぜかコピー先のタイムスタンプが新しくなってしまう。
                // （エクスプローラでも同じ）
                fileInfo.CopyTo(dstFilePath, true);

                // タイムスタンプを取得
                //DateTime lastWriteTime = System.IO.File.GetLastWriteTime(srcFilePath);

                // 新しいファイルのタイムスタンプを設定
                //File.SetLastWriteTime(dstFilePath, lastWriteTime);

                if (libLog != null)
                {
                    libLog.WriteLine("ファイル（" + srcFilePath + "）をファイル（" + dstFilePath + "）にコピーしました。");
                }

                return true;
            }
            catch(Exception)
            {
                if (libLog != null)
                {
                    libLog.WriteLineError("ファイル（" + srcFilePath + "）をファイル（" + dstFilePath + "）にコピー失敗しました。");
                }

                return false;
            }
        }
    }
}
