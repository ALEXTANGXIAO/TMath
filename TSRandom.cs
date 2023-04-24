using System;

namespace TMath
{
    public class TSRandom
    {
        private const int N = 624;
        private const int M = 397;
        private const uint MATRIX_A = 2567483615;
        private const uint UPPER_MASK = 2147483648;
        private const uint LOWER_MASK = 2147483647;
        private const int MAX_RAND_INT = 2147483647;
        private uint[] mag01 = new uint[2] { 0U, 2567483615U };
        private uint[] mt = new uint[624];
        private int mti = 625;

        public static TSRandom New(int seed) => new TSRandom(seed);

        private TSRandom() => this.init_genrand((uint)DateTime.Now.Millisecond);

        private TSRandom(int seed) => this.init_genrand((uint)seed);

        private TSRandom(int[] init)
        {
            uint[] init_key = new uint[init.Length];
            for (int index = 0; index < init.Length; ++index)
                init_key[index] = (uint)init[index];
            this.init_by_array(init_key, (uint)init_key.Length);
        }

        public static int MaxRandomInt => int.MaxValue;

        public int Next() => this.genrand_int31();

        public int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                int num = maxValue;
                maxValue = minValue;
                minValue = num;
            }

            int num1 = maxValue - minValue;
            return minValue + this.Next() % num1;
        }

        public FP Next(FP min, FP max)
        {
            if (min == max)
                return min;
            if (min > max)
            {
                FP fp = max;
                max = min;
                min = fp;
            }

            return this.NextFP() * (max - min + FP.Precision) + min;
        }

        public FP Next01() => this.Next(FP.Zero, FP.One);

        public TSVector2 RandomInsideCircle(FP radius) => new TSVector2()
        {
            x = this.Next(-radius, radius),
            y = this.Next(-radius, radius)
        };

        public FP NextFP() => (FP)this.Next() / (FP)TSRandom.MaxRandomInt;

        private void init_genrand(uint s)
        {
            this.mt[0] = s & uint.MaxValue;
            for (this.mti = 1; this.mti < 624; ++this.mti)
            {
                this.mt[this.mti] = (uint)((ulong)(uint)(1812433253 * ((int)this.mt[this.mti - 1] ^ (int)(this.mt[this.mti - 1] >> 30))) + (ulong)this.mti);
                this.mt[this.mti] &= uint.MaxValue;
            }
        }

        private void init_by_array(uint[] init_key, uint key_length)
        {
            this.init_genrand(19650218U);
            int index1 = 1;
            int index2 = 0;
            for (int index3 = 624U > key_length ? 624 : (int)key_length; index3 > 0; --index3)
            {
                this.mt[index1] = (uint)((ulong)((this.mt[index1] ^ (uint)(((int)this.mt[index1 - 1] ^ (int)(this.mt[index1 - 1] >> 30)) * 1664525)) + init_key[index2]) +
                                         (ulong)index2);
                this.mt[index1] &= uint.MaxValue;
                ++index1;
                ++index2;
                if (index1 >= 624)
                {
                    this.mt[0] = this.mt[623];
                    index1 = 1;
                }

                if ((long)index2 >= (long)key_length)
                    index2 = 0;
            }

            for (int index4 = 623; index4 > 0; --index4)
            {
                this.mt[index1] = (uint)((ulong)(this.mt[index1] ^ (uint)(((int)this.mt[index1 - 1] ^ (int)(this.mt[index1 - 1] >> 30)) * 1566083941)) - (ulong)index1);
                this.mt[index1] &= uint.MaxValue;
                ++index1;
                if (index1 >= 624)
                {
                    this.mt[0] = this.mt[623];
                    index1 = 1;
                }
            }

            this.mt[0] = 2147483648U;
        }

        private uint genrand_int32()
        {
            if (this.mti >= 624)
            {
                if (this.mti == 625)
                    this.init_genrand(5489U);
                int index;
                for (index = 0; index < 227; ++index)
                {
                    uint num = (uint)((int)this.mt[index] & int.MinValue | (int)this.mt[index + 1] & int.MaxValue);
                    this.mt[index] = this.mt[index + 397] ^ num >> 1 ^ this.mag01[(int)num & 1];
                }

                for (; index < 623; ++index)
                {
                    uint num = (uint)((int)this.mt[index] & int.MinValue | (int)this.mt[index + 1] & int.MaxValue);
                    this.mt[index] = this.mt[index - 227] ^ num >> 1 ^ this.mag01[(int)num & 1];
                }

                uint num1 = (uint)((int)this.mt[623] & int.MinValue | (int)this.mt[0] & int.MaxValue);
                this.mt[623] = this.mt[396] ^ num1 >> 1 ^ this.mag01[(int)num1 & 1];
                this.mti = 0;
            }

            uint num2 = this.mt[this.mti++];
            uint num3 = num2 ^ num2 >> 11;
            uint num4 = num3 ^ (uint)((int)num3 << 7 & -1658038656);
            uint num5 = num4 ^ (uint)((int)num4 << 15 & -272236544);
            return num5 ^ num5 >> 18;
        }

        private int genrand_int31() => (int)(this.genrand_int32() >> 1);

        private FP genrand_FP() => (FP)(long)this.genrand_int32() * (FP.One / (FP)(long)uint.MaxValue);

        private double genrand_real1() => (double)this.genrand_int32() * 2.3283064370807974E-10;

        private double genrand_real2() => (double)this.genrand_int32() * 2.3283064365386963E-10;

        private double genrand_real3() => ((double)this.genrand_int32() + 0.5) * 2.3283064365386963E-10;

        private double genrand_res53() => ((double)(this.genrand_int32() >> 5) * 67108864.0 + (double)(this.genrand_int32() >> 6)) * 1.1102230246251565E-16;
    }
}