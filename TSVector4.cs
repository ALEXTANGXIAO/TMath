using System;

namespace TMath
{
    [Serializable]
    public struct TSVector4
    {
        private static FP ZeroEpsilonSq = TSMath.Epsilon;
        internal static TSVector4 InternalZero;
        public FP x;
        public FP y;
        public FP z;
        public FP w;
        public static readonly TSVector4 zero;
        public static readonly TSVector4 one = new TSVector4(1, 1, 1, 1);
        public static readonly TSVector4 MinValue;
        public static readonly TSVector4 MaxValue;

        static TSVector4()
        {
            TSVector4.zero = new TSVector4(0, 0, 0, 0);
            TSVector4.MinValue = new TSVector4(FP.MinValue);
            TSVector4.MaxValue = new TSVector4(FP.MaxValue);
            TSVector4.InternalZero = TSVector4.zero;
        }

        public static TSVector4 Abs(TSVector4 other) => new TSVector4(FP.Abs(other.x), FP.Abs(other.y), FP.Abs(other.z), FP.Abs(other.z));

        public FP sqrMagnitude => this.x * this.x + this.y * this.y + this.z * this.z + this.w * this.w;

        public FP magnitude => FP.Sqrt(this.sqrMagnitude);

        public static TSVector4 ClampMagnitude(TSVector4 vector, FP maxLength) => TSVector4.Normalize(vector) * maxLength;

        public TSVector4 normalized
        {
            get
            {
                TSVector4 normalized = new TSVector4(this.x, this.y, this.z, this.w);
                normalized.Normalize();
                return normalized;
            }
        }

        public TSVector4(int x, int y, int z, int w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public TSVector4(FP x, FP y, FP z, FP w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public void Scale(TSVector4 other)
        {
            this.x *= other.x;
            this.y *= other.y;
            this.z *= other.z;
            this.w *= other.w;
        }

        public void Set(FP x, FP y, FP z, FP w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public TSVector4(FP xyzw)
        {
            this.x = xyzw;
            this.y = xyzw;
            this.z = xyzw;
            this.w = xyzw;
        }

        public static TSVector4 Lerp(TSVector4 from, TSVector4 to, FP percent) => from + (to - from) * percent;

        public override string ToString() => string.Format("({0:f1}, {1:f1}, {2:f1}, {3:f1})", this.x.AsFloat(), this.y.AsFloat(), this.z.AsFloat(),
            this.w.AsFloat());

        public override bool Equals(object obj) => obj is TSVector4 tsVector4 && this.x == tsVector4.x && this.y == tsVector4.y && this.z == tsVector4.z && this.w == tsVector4.w;

        public static TSVector4 Scale(TSVector4 vecA, TSVector4 vecB)
        {
            TSVector4 tsVector4;
            tsVector4.x = vecA.x * vecB.x;
            tsVector4.y = vecA.y * vecB.y;
            tsVector4.z = vecA.z * vecB.z;
            tsVector4.w = vecA.w * vecB.w;
            return tsVector4;
        }

        public static bool operator ==(TSVector4 value1, TSVector4 value2) => value1.x == value2.x && value1.y == value2.y && value1.z == value2.z && value1.w == value2.w;

        public static bool operator !=(TSVector4 value1, TSVector4 value2) => !(value1.x == value2.x) || !(value1.y == value2.y) || !(value1.z == value2.z) || value1.w != value2.w;

        public static TSVector4 Min(TSVector4 value1, TSVector4 value2)
        {
            TSVector4 result;
            TSVector4.Min(ref value1, ref value2, out result);
            return result;
        }

        public static void Min(ref TSVector4 value1, ref TSVector4 value2, out TSVector4 result)
        {
            result.x = value1.x < value2.x ? value1.x : value2.x;
            result.y = value1.y < value2.y ? value1.y : value2.y;
            result.z = value1.z < value2.z ? value1.z : value2.z;
            result.w = value1.w < value2.w ? value1.w : value2.w;
        }

        public static TSVector4 Max(TSVector4 value1, TSVector4 value2)
        {
            TSVector4 result;
            TSVector4.Max(ref value1, ref value2, out result);
            return result;
        }

        public static FP Distance(TSVector4 v1, TSVector4 v2) =>
            FP.Sqrt((v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y) + (v1.z - v2.z) * (v1.z - v2.z) + (v1.w - v2.w) * (v1.w - v2.w));

        public static void Max(ref TSVector4 value1, ref TSVector4 value2, out TSVector4 result)
        {
            result.x = value1.x > value2.x ? value1.x : value2.x;
            result.y = value1.y > value2.y ? value1.y : value2.y;
            result.z = value1.z > value2.z ? value1.z : value2.z;
            result.w = value1.w > value2.w ? value1.w : value2.w;
        }

        public void MakeZero()
        {
            this.x = FP.Zero;
            this.y = FP.Zero;
            this.z = FP.Zero;
            this.w = FP.Zero;
        }

        public bool IsZero() => this.sqrMagnitude == FP.Zero;

        public bool IsNearlyZero() => this.sqrMagnitude < TSVector4.ZeroEpsilonSq;

        public static TSVector4 Transform(TSVector4 position, TSMatrix4x4 matrix)
        {
            TSVector4 result;
            TSVector4.Transform(ref position, ref matrix, out result);
            return result;
        }

        public static TSVector4 Transform(TSVector position, TSMatrix4x4 matrix)
        {
            TSVector4 result;
            TSVector4.Transform(ref position, ref matrix, out result);
            return result;
        }

        public static void Transform(ref TSVector vector, ref TSMatrix4x4 matrix, out TSVector4 result)
        {
            result.x = vector.x * matrix.M11 + vector.y * matrix.M12 + vector.z * matrix.M13 + matrix.M14;
            result.y = vector.x * matrix.M21 + vector.y * matrix.M22 + vector.z * matrix.M23 + matrix.M24;
            result.z = vector.x * matrix.M31 + vector.y * matrix.M32 + vector.z * matrix.M33 + matrix.M34;
            result.w = vector.x * matrix.M41 + vector.y * matrix.M42 + vector.z * matrix.M43 + matrix.M44;
        }

        public static void Transform(
            ref TSVector4 vector,
            ref TSMatrix4x4 matrix,
            out TSVector4 result)
        {
            result.x = vector.x * matrix.M11 + vector.y * matrix.M12 + vector.z * matrix.M13 + vector.w * matrix.M14;
            result.y = vector.x * matrix.M21 + vector.y * matrix.M22 + vector.z * matrix.M23 + vector.w * matrix.M24;
            result.z = vector.x * matrix.M31 + vector.y * matrix.M32 + vector.z * matrix.M33 + vector.w * matrix.M34;
            result.w = vector.x * matrix.M41 + vector.y * matrix.M42 + vector.z * matrix.M43 + vector.w * matrix.M44;
        }

        public static FP Dot(TSVector4 vector1, TSVector4 vector2) => TSVector4.Dot(ref vector1, ref vector2);

        public static FP Dot(ref TSVector4 vector1, ref TSVector4 vector2) => vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z + vector1.w * vector2.w;

        public static TSVector4 Add(TSVector4 value1, TSVector4 value2)
        {
            TSVector4 result;
            TSVector4.Add(ref value1, ref value2, out result);
            return result;
        }

        public static void Add(ref TSVector4 value1, ref TSVector4 value2, out TSVector4 result)
        {
            result.x = value1.x + value2.x;
            result.y = value1.y + value2.y;
            result.z = value1.z + value2.z;
            result.w = value1.w + value2.w;
        }

        public static TSVector4 Divide(TSVector4 value1, FP scaleFactor)
        {
            TSVector4 result;
            TSVector4.Divide(ref value1, scaleFactor, out result);
            return result;
        }

        public static void Divide(ref TSVector4 value1, FP scaleFactor, out TSVector4 result)
        {
            result.x = value1.x / scaleFactor;
            result.y = value1.y / scaleFactor;
            result.z = value1.z / scaleFactor;
            result.w = value1.w / scaleFactor;
        }

        public static TSVector4 Subtract(TSVector4 value1, TSVector4 value2)
        {
            TSVector4 result;
            TSVector4.Subtract(ref value1, ref value2, out result);
            return result;
        }

        public static void Subtract(ref TSVector4 value1, ref TSVector4 value2, out TSVector4 result)
        {
            result.x = value1.x - value2.x;
            result.y = value1.y - value2.y;
            result.z = value1.z - value2.z;
            result.w = value1.w - value2.w;
        }

        public override int GetHashCode() => this.x.GetHashCode() ^ this.y.GetHashCode() ^ this.z.GetHashCode() ^ this.w.GetHashCode();

        public void Negate()
        {
            this.x = -this.x;
            this.y = -this.y;
            this.z = -this.z;
            this.w = -this.w;
        }

        public static TSVector4 Negate(TSVector4 value)
        {
            TSVector4 result;
            TSVector4.Negate(ref value, out result);
            return result;
        }

        public static void Negate(ref TSVector4 value, out TSVector4 result)
        {
            result.x = -value.x;
            result.y = -value.y;
            result.z = -value.z;
            result.w = -value.w;
        }

        public static TSVector4 Normalize(TSVector4 value)
        {
            TSVector4 result;
            TSVector4.Normalize(ref value, out result);
            return result;
        }

        public void Normalize()
        {
            FP x = this.x * this.x + this.y * this.y + this.z * this.z + this.w * this.w;
            FP fp = FP.One / FP.Sqrt(x);
            this.x *= fp;
            this.y *= fp;
            this.z *= fp;
            this.w *= fp;
        }

        public static void Normalize(ref TSVector4 value, out TSVector4 result)
        {
            FP x = value.x * value.x + value.y * value.y + value.z * value.z + value.w * value.w;
            FP fp = FP.One / FP.Sqrt(x);
            result.x = value.x * fp;
            result.y = value.y * fp;
            result.z = value.z * fp;
            result.w = value.w * fp;
        }

        public static void Swap(ref TSVector4 vector1, ref TSVector4 vector2)
        {
            FP x = vector1.x;
            vector1.x = vector2.x;
            vector2.x = x;
            FP y = vector1.y;
            vector1.y = vector2.y;
            vector2.y = y;
            FP z = vector1.z;
            vector1.z = vector2.z;
            vector2.z = z;
            FP w = vector1.w;
            vector1.w = vector2.w;
            vector2.w = w;
        }

        public static TSVector4 Multiply(TSVector4 value1, FP scaleFactor)
        {
            TSVector4 result;
            TSVector4.Multiply(ref value1, scaleFactor, out result);
            return result;
        }

        public static void Multiply(ref TSVector4 value1, FP scaleFactor, out TSVector4 result)
        {
            result.x = value1.x * scaleFactor;
            result.y = value1.y * scaleFactor;
            result.z = value1.z * scaleFactor;
            result.w = value1.w * scaleFactor;
        }

        public static FP operator *(TSVector4 value1, TSVector4 value2) => TSVector4.Dot(ref value1, ref value2);

        public static TSVector4 operator *(TSVector4 value1, FP value2)
        {
            TSVector4 result;
            TSVector4.Multiply(ref value1, value2, out result);
            return result;
        }

        public static TSVector4 operator *(FP value1, TSVector4 value2)
        {
            TSVector4 result;
            TSVector4.Multiply(ref value2, value1, out result);
            return result;
        }

        public static TSVector4 operator -(TSVector4 value1, TSVector4 value2)
        {
            TSVector4 result;
            TSVector4.Subtract(ref value1, ref value2, out result);
            return result;
        }

        public static TSVector4 operator +(TSVector4 value1, TSVector4 value2)
        {
            TSVector4 result;
            TSVector4.Add(ref value1, ref value2, out result);
            return result;
        }

        public static TSVector4 operator /(TSVector4 value1, FP value2)
        {
            TSVector4 result;
            TSVector4.Divide(ref value1, value2, out result);
            return result;
        }

        public TSVector2 ToTSVector2() => new TSVector2(this.x, this.y);

        public TSVector ToTSVector() => new TSVector(this.x, this.y, this.z);
    }
}