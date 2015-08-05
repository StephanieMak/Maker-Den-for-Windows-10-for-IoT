using AdafruitMatrix;
using Glovebox.IoT;
using System;
using System.Threading.Tasks;

namespace Glovebox.Adafruit.Mini8x8Matrix {
    public class AdaFruitMatrixRun : Adafruit8x8Matrix {

        public DoCycle[] cycles;


        public AdaFruitMatrixRun(string name)
            : base(name) {

            CreateCyclesCollection();

            Task.Run(() => StartMiniMatrixThread());

        }

        public void StartMiniMatrixThread() {
            while (true) {
                for (int i = 0; i < cycles.Length; i++) {
                    ExecuteCycle(cycles[i]);
                }
            }
        }

        private void ExecuteCycle(DoCycle cycle) {
            cycle();
        }

        private void CreateCyclesCollection() {
            cycles = new DoCycle[] {

            new DoCycle(IPAddress),
            new DoCycle(HappyBirthday),
            new DoCycle(AlphaNumeric),
            new DoCycle(ShowSymbols),
            new DoCycle(Hearts),
            new DoCycle(FollowMe),
            };
        }

        public void IPAddress() {
            ScrollStringInFromRight(Util.GetIPAddress(), 100);
            Delay(200);
        }

        public void HappyBirthday() {
            ScrollStringInFromRight("Happy Birthday", 100);
            ScrollSymbolInFromRight(new Symbols[] { Symbols.Heart, Symbols.Heart }, 100);
        }

        public void AlphaNumeric() {
            for (int i = 0; i < fontSimple.Length; i++) {
                DrawBitmap(fontSimple[i]);
                FrameDraw();
                Delay(100);
            }
        }

        private void ShowSymbols() {
            foreach (Symbols suit in Enum.GetValues(typeof(Symbols))) {
                DrawSymbol(suit);
                FrameDraw();
                Delay(100);
            }
        }

        public void Hearts() {
            DrawSymbol(Symbols.Heart);
            FrameDraw();
            Delay(50);

            for (int i = 0; i < 4; i++) {
                for (ushort c = 0; c < Columns; c++) {
                    FrameRollRight();
                    FrameDraw();
                    Delay(50);
                }
            }

            for (int c = 0; c < 4; c++) {
                for (int i = 0; i < Rows; i++) {
                    RowRollUp();
                    FrameDraw();
                    Delay(50);
                }
            }

            for (int i = 0; i < 4; i++) {

                for (ushort c = 0; c < Columns; c++) {
                    FrameRollLeft();
                    FrameDraw();
                    Delay(50);
                }
            }

            for (int c = 0; c < 4; c++) {
                for (int i = 0; i < Rows; i++) {
                    RowRollDown();
                    FrameDraw();
                    Delay(50);
                }
            }

            for (int j = 0; j < 4; j++) {
                for (int i = 0; i < Rows; i++) {
                    ColumnRollLeft(0);
                    ColumnRollRight(1);
                    ColumnRollLeft(2);
                    ColumnRollRight(3);
                    ColumnRollLeft(4);
                    ColumnRollRight(5);
                    ColumnRollLeft(6);
                    ColumnRollRight(7);
                    FrameDraw();
                    Delay(100);
                }
                Delay(500);
            }
        }

        public void FollowMe() {
            for (int j = 0; j < 2; j++) {
                for (int i = 0; i < 64; i++) {
                    FrameSet(i, true);
                    FrameSet((63 - i), true);
                    FrameDraw();
                    Delay(15);
                    FrameSet(i, false);
                    FrameSet((63 - i), false);
                    FrameDraw();
                    Delay(15);
                }
            }
        }

        private void Delay(int milliseconds) {
            Task.Delay(milliseconds).Wait();
        }
    }
}

