﻿/*
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

namespace CommonClass
{
    /// <summary>
    /// ストップウォッチ等、ダイアグ関連のクラス
    /// </summary>
    public class LibDiag
    {
        static Stopwatch stopwatch = null;

        /// <summary>
        /// ストップウォッチをスタートさせる。
        /// </summary>
        public static void StopwatchStart()
        {
            // Stopwatchのインスタンスを作成
            stopwatch = new Stopwatch();

            // 計測開始
            stopwatch.Start();
        }

        /// <summary>
        /// ストップウォッチをストップさせる。
        /// </summary>
        /// <returns>long | 計測時間（ms）</returns>
        public static long StopwatchStop()
        {
            long ms;

            // 計測停止
            stopwatch.Stop();

            ms = stopwatch.ElapsedMilliseconds;

            // メモリの解放
            stopwatch.Reset();

            return ms;
        }
    }
}
