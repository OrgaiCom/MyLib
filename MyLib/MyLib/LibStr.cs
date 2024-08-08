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

namespace CommonClass
{
    /// <summary>
    /// 文字列関連のクラス
    /// </summary>
    public class LibStr
    {
        /// <summary>
        /// 指定された文字列内のエスケープシーケンスを変換した文字列を返します。
        /// 文字列内の"\\"を'\'に変換します。
        /// 文字列内の"\n"をLFに変換します。
        /// 文字列内の"\r"をCRに変換します。
        /// 文字列内の"\t"をタブに変換します。
        /// 文字列内の"\0"を空文字に変換します。
        /// </summary>
        /// <param name="str">変換する文字列</param>
        /// <returns>string | 変換後の文字列</returns>
        public string ConvertEscapeSequence(string str)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '\\' 
                    && i + 1 < str.Length)
                {
                    switch (str[i + 1])
                    {
                        case '\\':
                            result.Append('\\');
                            i++;
                            break;

                        case 'n':
                            result.Append('\n');
                            i++;
                            break;

                        case 'r':
                            result.Append('\r');
                            i++;
                            break;

                        case 't':
                            result.Append('\t');
                            i++;
                            break;

                        case '0':
                            // 空文字に変換するので何も追加しない
                            i++;
                            break;

                        default:
                            result.Append(str[i]);
                            break;
                    }
                }
                else
                {
                    result.Append(str[i]);
                }
            }

            return result.ToString();
        }
    }
}
