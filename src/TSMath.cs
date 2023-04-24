using System;

namespace TMath
{
    public sealed class TSMath
    {
        public static FP Pi = FP.Pi;
        public static FP PiOver2 = FP.PiOver2;
        public static FP Epsilon = FP.Epsilon;
        public static FP Deg2Rad = FP.Deg2Rad;
        public static FP Rad2Deg = FP.Rad2Deg;
        public static FP Infinity = FP.MaxValue;

        public static FP Sqrt(FP number) => FP.Sqrt(number);

        public static FP Max(FP val1, FP val2) => !(val1 > val2) ? val2 : val1;

        public static FP Min(FP val1, FP val2) => !(val1 < val2) ? val2 : val1;

        public static FP Max(FP val1, FP val2, FP val3)
        {
            FP fp = val1 > val2 ? val1 : val2;
            return !(fp > val3) ? val3 : fp;
        }

        public static FP Clamp(FP value, FP min, FP max)
        {
            if (value < min)
            {
                value = min;
                return value;
            }

            if (value > max)
                value = max;
            return value;
        }

        public static FP Clamp01(FP value)
        {
            if (value < FP.Zero)
                return FP.Zero;
            return value > FP.One ? FP.One : value;
        }

        public static void Absolute(ref TSMatrix matrix, out TSMatrix result)
        {
            result.M11 = FP.Abs(matrix.M11);
            result.M12 = FP.Abs(matrix.M12);
            result.M13 = FP.Abs(matrix.M13);
            result.M21 = FP.Abs(matrix.M21);
            result.M22 = FP.Abs(matrix.M22);
            result.M23 = FP.Abs(matrix.M23);
            result.M31 = FP.Abs(matrix.M31);
            result.M32 = FP.Abs(matrix.M32);
            result.M33 = FP.Abs(matrix.M33);
        }

        public static FP Sin(FP value) => FP.Sin(value);

        public static FP Cos(FP value) => FP.Cos(value);

        public static FP Tan(FP value) => FP.Tan(value);

        public static FP Asin(FP value) => FP.Asin(value);

        public static FP Acos(FP value) => FP.Acos(value);

        public static FP Atan(FP value) => FP.Atan(value);

        public static FP Atan2(FP y, FP x) => FP.Atan2(y, x);

        public static FP Floor(FP value) => FP.Floor(value);

        public static FP Ceiling(FP value) => FP.Ceiling(value);

        public static FP Round(FP value) => FP.Round(value);

        public static int Sign(FP value) => FP.Sign(value);

        public static FP Abs(FP value) => FP.Abs(value);

        public static FP Barycentric(FP value1, FP value2, FP value3, FP amount1, FP amount2) => value1 + (value2 - value1) * amount1 + (value3 - value1) * amount2;

        public static FP CatmullRom(FP value1, FP value2, FP value3, FP value4, FP amount)
        {
            FP fp1 = amount * amount;
            FP fp2 = fp1 * amount;
            return FP.Half * ((FP)2 * value2 + (value3 - value1) * amount + ((FP)2 * value1 - (FP)5 * value2 + (FP)4 * value3 - value4) * fp1 +
                              ((FP)3 * value2 - value1 - (FP)3 * value3 + value4) * fp2);
        }

        public static FP Distance(FP value1, FP value2) => FP.Abs(value1 - value2);

        public static FP Hermite(FP value1, FP tangent1, FP value2, FP tangent2, FP amount)
        {
            FP fp1 = value1;
            FP fp2 = value2;
            FP fp3 = tangent1;
            FP fp4 = tangent2;
            FP fp5 = amount;
            FP fp6 = fp5 * fp5 * fp5;
            FP fp7 = fp5 * fp5;
            return !(amount == FP.Zero)
                ? (!(amount == FP.One) ? ((FP)2 * fp1 - (FP)2 * fp2 + fp4 + fp3) * fp6 + ((FP)3 * fp2 - (FP)3 * fp1 - (FP)2 * fp3 - fp4) * fp7 + fp3 * fp5 + fp1 : value2)
                : value1;
        }

        public static FP Lerp(FP value1, FP value2, FP amount) => value1 + (value2 - value1) * TSMath.Clamp01(amount);

        public static FP InverseLerp(FP value1, FP value2, FP amount) => value1 != value2 ? TSMath.Clamp01((amount - value1) / (value2 - value1)) : FP.Zero;

        public static FP SmoothStep(FP value1, FP value2, FP amount)
        {
            FP amount1 = TSMath.Clamp(amount, FP.Zero, FP.One);
            return TSMath.Hermite(value1, FP.Zero, value2, FP.Zero, amount1);
        }

        internal static FP Pow2(FP x)
        {
            if (x.RawValue == 0L)
                return FP.One;
            bool flag = x.RawValue < 0L;
            if (flag)
                x = -x;
            if (x == FP.One)
                return !flag ? (FP)2 : FP.One / (FP)2;
            if (x >= FP.Log2Max)
                return !flag ? FP.MaxValue : FP.One / FP.MaxValue;
            if (x <= FP.Log2Min)
                return !flag ? FP.Zero : FP.MaxValue;
            int num1 = (int)(long)TSMath.Floor(x);
            x = FP.FromRaw(x.RawValue & (long)uint.MaxValue);
            FP one = FP.One;
            FP y = FP.One;
            int num2 = 1;
            while (y.RawValue != 0L)
            {
                y = FP.FastMul(FP.FastMul(x, y), FP.Ln2) / (FP)num2;
                one += y;
                ++num2;
            }

            FP fp = FP.FromRaw(one.RawValue << num1);
            if (flag)
                fp = FP.One / fp;
            return fp;
        }

        internal static FP Log2(FP x)
        {
            if (x.RawValue <= 0L)
                throw new ArgumentOutOfRangeException("Non-positive value passed to Ln", nameof(x));
            long num = 2147483648;
            long rawValue1 = 0;
            long rawValue2 = x.RawValue;
            while (rawValue2 < 4294967296L)
            {
                rawValue2 <<= 1;
                rawValue1 -= 4294967296L;
            }

            while (rawValue2 >= 8589934592L)
            {
                rawValue2 >>= 1;
                rawValue1 += 4294967296L;
            }

            FP fp = FP.FromRaw(rawValue2);
            for (int index = 0; index < 32; ++index)
            {
                fp = FP.FastMul(fp, fp);
                if (fp.RawValue >= 8589934592L)
                {
                    fp = FP.FromRaw(fp.RawValue >> 1);
                    rawValue1 += num;
                }

                num >>= 1;
            }

            return FP.FromRaw(rawValue1);
        }

        public static FP Ln(FP x) => FP.FastMul(TSMath.Log2(x), FP.Ln2);

        public static FP Pow(FP b, FP exp)
        {
            if (b == FP.One || exp.RawValue == 0L)
                return FP.One;
            if (b.RawValue == 0L)
                return exp.RawValue < 0L ? FP.MaxValue : FP.Zero;
            FP fp = TSMath.Log2(b);
            return TSMath.Pow2(exp * fp);
        }

        public static FP MoveTowards(FP current, FP target, FP maxDelta) =>
            TSMath.Abs(target - current) <= maxDelta ? target : current + (FP)TSMath.Sign(target - current) * maxDelta;

        public static FP Repeat(FP t, FP length) => t - TSMath.Floor(t / length) * length;

        public static FP DeltaAngle(FP current, FP target)
        {
            FP fp = TSMath.Repeat(target - current, (FP)360L);
            if (fp > (FP)180L)
                fp -= (FP)360L;
            return fp;
        }

        public static FP MoveTowardsAngle(FP current, FP target, FP maxDelta)
        {
            target = current + TSMath.DeltaAngle(current, target);
            return TSMath.MoveTowards(current, target, maxDelta);
        }

        public static FP SmoothDamp(
            FP current,
            FP target,
            ref FP currentVelocity,
            FP smoothTime,
            FP maxSpeed)
        {
            FP en2 = FP.EN2;
            return TSMath.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, en2);
        }

        public static FP SmoothDamp(FP current, FP target, ref FP currentVelocity, FP smoothTime)
        {
            FP en2 = FP.EN2;
            FP maxSpeed = -FP.MaxValue;
            return TSMath.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, en2);
        }

        public static FP SmoothDamp(
            FP current,
            FP target,
            ref FP currentVelocity,
            FP smoothTime,
            FP maxSpeed,
            FP deltaTime)
        {
            smoothTime = TSMath.Max(FP.EN4, smoothTime);
            FP fp1 = (FP)2L / smoothTime;
            FP fp2 = fp1 * deltaTime;
            FP fp3 = FP.One / (FP.One + fp2 + (FP)0L * fp2 * fp2 + (FP)0L * fp2 * fp2 * fp2);
            FP fp4 = current - target;
            FP fp5 = target;
            FP max = maxSpeed * smoothTime;
            FP fp6 = TSMath.Clamp(fp4, -max, max);
            target = current - fp6;
            FP fp7 = (currentVelocity + fp1 * fp6) * deltaTime;
            currentVelocity = (currentVelocity - fp1 * fp7) * fp3;
            FP fp8 = target + (fp6 + fp7) * fp3;
            if (fp5 - current > FP.Zero == fp8 > fp5)
            {
                fp8 = fp5;
                currentVelocity = (fp8 - fp5) / deltaTime;
            }

            return fp8;
        }
    }
}