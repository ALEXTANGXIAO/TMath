using System;

namespace TMath
{
    [Serializable]
    public struct TSVector
    {
        private static FP ZeroEpsilonSq = TSMath.Epsilon;
        internal static TSVector InternalZero;
        internal static TSVector Arbitrary;
        public FP x;
        public FP y;
        public FP z;
        public static readonly TSVector zero;
        public static readonly TSVector left;
        public static readonly TSVector right;
        public static readonly TSVector up;
        public static readonly TSVector down;
        public static readonly TSVector back;
        public static readonly TSVector forward;
        public static readonly TSVector one = new TSVector(1, 1, 1);
        public static readonly TSVector MinValue;
        public static readonly TSVector MaxValue;

        public static TSVector FromFloat(float x, float y, float z) => new TSVector(FP.FromFloat(x), FP.FromFloat(y), FP.FromFloat(z));

        static TSVector()
        {
            TSVector.zero = new TSVector(0, 0, 0);
            TSVector.left = new TSVector(-1, 0, 0);
            TSVector.right = new TSVector(1, 0, 0);
            TSVector.up = new TSVector(0, 1, 0);
            TSVector.down = new TSVector(0, -1, 0);
            TSVector.back = new TSVector(0, 0, -1);
            TSVector.forward = new TSVector(0, 0, 1);
            TSVector.MinValue = new TSVector(FP.MinValue);
            TSVector.MaxValue = new TSVector(FP.MaxValue);
            TSVector.Arbitrary = new TSVector(1, 1, 1);
            TSVector.InternalZero = TSVector.zero;
        }

        public static TSVector Abs(TSVector other) => new TSVector(FP.Abs(other.x), FP.Abs(other.y), FP.Abs(other.z));

        public FP sqrMagnitude => this.x * this.x + this.y * this.y + this.z * this.z;

        public FP magnitude => FP.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);

        public static TSVector ClampMagnitude(TSVector vector, FP maxLength) => TSVector.Normalize(vector) * maxLength;

        public TSVector normalized
        {
            get
            {
                TSVector normalized = new TSVector(this.x, this.y, this.z);
                normalized.Normalize();
                return normalized;
            }
        }

        public TSVector(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public TSVector(FP x, FP y, FP z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Scale(TSVector other)
        {
            this.x *= other.x;
            this.y *= other.y;
            this.z *= other.z;
        }

        public void Set(FP x, FP y, FP z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public TSVector(FP xyz)
        {
            this.x = xyz;
            this.y = xyz;
            this.z = xyz;
        }

        public static TSVector Lerp(TSVector from, TSVector to, FP percent) => from + (to - from) * percent;

        public override string ToString() => string.Format("({0:f1}, {1:f1}, {2:f1})", this.x.AsFloat(), this.y.AsFloat(), this.z.AsFloat());

        public override bool Equals(object obj) => obj is TSVector tsVector && this.x == tsVector.x && this.y == tsVector.y && this.z == tsVector.z;

        public static TSVector Scale(TSVector vecA, TSVector vecB)
        {
            TSVector tsVector;
            tsVector.x = vecA.x * vecB.x;
            tsVector.y = vecA.y * vecB.y;
            tsVector.z = vecA.z * vecB.z;
            return tsVector;
        }

        public static bool operator ==(TSVector value1, TSVector value2) => value1.x == value2.x && value1.y == value2.y && value1.z == value2.z;

        public static bool operator !=(TSVector value1, TSVector value2) => !(value1.x == value2.x) || !(value1.y == value2.y) || value1.z != value2.z;

        public static TSVector Min(TSVector value1, TSVector value2)
        {
            TSVector result;
            TSVector.Min(ref value1, ref value2, out result);
            return result;
        }

        public static void Min(ref TSVector value1, ref TSVector value2, out TSVector result)
        {
            result.x = value1.x < value2.x ? value1.x : value2.x;
            result.y = value1.y < value2.y ? value1.y : value2.y;
            result.z = value1.z < value2.z ? value1.z : value2.z;
        }

        public static TSVector Max(TSVector value1, TSVector value2)
        {
            TSVector result;
            TSVector.Max(ref value1, ref value2, out result);
            return result;
        }

        public static FP Distance(TSVector v1, TSVector v2) => FP.Sqrt((v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y) + (v1.z - v2.z) * (v1.z - v2.z));

        public static FP DistanceSquare(TSVector v1, TSVector v2) => (v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y) + (v1.z - v2.z) * (v1.z - v2.z);

        public static void Max(ref TSVector value1, ref TSVector value2, out TSVector result)
        {
            result.x = value1.x > value2.x ? value1.x : value2.x;
            result.y = value1.y > value2.y ? value1.y : value2.y;
            result.z = value1.z > value2.z ? value1.z : value2.z;
        }

        public void MakeZero()
        {
            this.x = FP.Zero;
            this.y = FP.Zero;
            this.z = FP.Zero;
        }

        public bool IsZero() => this.sqrMagnitude == FP.Zero;

        public bool IsNearlyZero() => this.sqrMagnitude < TSVector.ZeroEpsilonSq;

        public static TSVector Transform(TSVector position, TSMatrix matrix)
        {
            TSVector result;
            TSVector.Transform(ref position, ref matrix, out result);
            return result;
        }

        public static void Transform(ref TSVector position, ref TSMatrix matrix, out TSVector result)
        {
            FP fp1 = position.x * matrix.M11 + position.y * matrix.M21 + position.z * matrix.M31;
            FP fp2 = position.x * matrix.M12 + position.y * matrix.M22 + position.z * matrix.M32;
            FP fp3 = position.x * matrix.M13 + position.y * matrix.M23 + position.z * matrix.M33;
            result.x = fp1;
            result.y = fp2;
            result.z = fp3;
        }

        public static void TransposedTransform(
            ref TSVector position,
            ref TSMatrix matrix,
            out TSVector result)
        {
            FP fp1 = position.x * matrix.M11 + position.y * matrix.M12 + position.z * matrix.M13;
            FP fp2 = position.x * matrix.M21 + position.y * matrix.M22 + position.z * matrix.M23;
            FP fp3 = position.x * matrix.M31 + position.y * matrix.M32 + position.z * matrix.M33;
            result.x = fp1;
            result.y = fp2;
            result.z = fp3;
        }

        public static FP Dot(TSVector vector1, TSVector vector2) => TSVector.Dot(ref vector1, ref vector2);

        public static bool IsSameDir(FP dotVal) => dotVal >= 96 * FP.EN2;

        public static FP Dot(ref TSVector vector1, ref TSVector vector2) => vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z;

        public static TSVector Project(TSVector vector, TSVector onNormal)
        {
            FP fp = TSVector.Dot(onNormal, onNormal);
            return fp < TSMath.Epsilon ? TSVector.zero : onNormal * TSVector.Dot(vector, onNormal) / fp;
        }

        public static TSVector ProjectOnPlane(TSVector vector, TSVector planeNormal) => vector - TSVector.Project(vector, planeNormal);

        public static FP Angle(TSVector from, TSVector to) => TSMath.Acos(TSMath.Clamp(TSVector.Dot(from.normalized, to.normalized), -4294967296L, 4294967296L)) * TSMath.Rad2Deg;

        public static FP SignedAngle(TSVector from, TSVector to, TSVector axis)
        {
            TSVector normalized1 = from.normalized;
            TSVector normalized2 = to.normalized;
            return TSMath.Acos(TSMath.Clamp(TSVector.Dot(normalized1, normalized2), -4294967296L, 4294967296L)) * TSMath.Rad2Deg *
                   TSMath.Sign(TSVector.Dot(axis, TSVector.Cross(normalized1, normalized2)));
        }

        public static TSVector Add(TSVector value1, TSVector value2)
        {
            TSVector result;
            TSVector.Add(ref value1, ref value2, out result);
            return result;
        }

        public static void Add(ref TSVector value1, ref TSVector value2, out TSVector result)
        {
            FP fp1 = value1.x + value2.x;
            FP fp2 = value1.y + value2.y;
            FP fp3 = value1.z + value2.z;
            result.x = fp1;
            result.y = fp2;
            result.z = fp3;
        }

        public static TSVector Divide(TSVector value1, FP scaleFactor)
        {
            TSVector result;
            TSVector.Divide(ref value1, scaleFactor, out result);
            return result;
        }

        public static void Divide(ref TSVector value1, FP scaleFactor, out TSVector result)
        {
            result.x = value1.x / scaleFactor;
            result.y = value1.y / scaleFactor;
            result.z = value1.z / scaleFactor;
        }

        public static TSVector Subtract(TSVector value1, TSVector value2)
        {
            TSVector result;
            TSVector.Subtract(ref value1, ref value2, out result);
            return result;
        }

        public static void Subtract(ref TSVector value1, ref TSVector value2, out TSVector result)
        {
            FP fp1 = value1.x - value2.x;
            FP fp2 = value1.y - value2.y;
            FP fp3 = value1.z - value2.z;
            result.x = fp1;
            result.y = fp2;
            result.z = fp3;
        }

        public static TSVector Cross(TSVector vector1, TSVector vector2)
        {
            TSVector result;
            TSVector.Cross(ref vector1, ref vector2, out result);
            return result;
        }

        public static void Cross(ref TSVector vector1, ref TSVector vector2, out TSVector result)
        {
            FP fp1 = vector1.y * vector2.z - vector1.z * vector2.y;
            FP fp2 = vector1.z * vector2.x - vector1.x * vector2.z;
            FP fp3 = vector1.x * vector2.y - vector1.y * vector2.x;
            result.x = fp1;
            result.y = fp2;
            result.z = fp3;
        }

        public override int GetHashCode() => this.x.GetHashCode() ^ this.y.GetHashCode() ^ this.z.GetHashCode();

        public void Negate()
        {
            this.x = -this.x;
            this.y = -this.y;
            this.z = -this.z;
        }

        public static TSVector Negate(TSVector value)
        {
            TSVector result;
            TSVector.Negate(ref value, out result);
            return result;
        }

        public static void Negate(ref TSVector value, out TSVector result)
        {
            FP fp1 = -value.x;
            FP fp2 = -value.y;
            FP fp3 = -value.z;
            result.x = fp1;
            result.y = fp2;
            result.z = fp3;
        }

        public static TSVector Normalize(TSVector value)
        {
            TSVector result;
            TSVector.Normalize(ref value, out result);
            return result;
        }

        public void Normalize()
        {
            FP x = this.x * this.x + this.y * this.y + this.z * this.z;
            if (x < FP.EN9)
                return;
            FP fp = FP.One / FP.Sqrt(x);
            this.x *= fp;
            this.y *= fp;
            this.z *= fp;
        }

        public static void Normalize(ref TSVector value, out TSVector result)
        {
            FP x = value.x * value.x + value.y * value.y + value.z * value.z;
            if (x < FP.EN9)
            {
                result = value;
            }
            else
            {
                FP fp = FP.One / FP.Sqrt(x);
                result.x = value.x * fp;
                result.y = value.y * fp;
                result.z = value.z * fp;
            }
        }

        public static void Swap(ref TSVector vector1, ref TSVector vector2)
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
        }

        public static TSVector Multiply(TSVector value1, FP scaleFactor)
        {
            TSVector result;
            TSVector.Multiply(ref value1, scaleFactor, out result);
            return result;
        }

        public static void Multiply(ref TSVector value1, FP scaleFactor, out TSVector result)
        {
            result.x = value1.x * scaleFactor;
            result.y = value1.y * scaleFactor;
            result.z = value1.z * scaleFactor;
        }

        public static TSVector operator %(TSVector value1, TSVector value2)
        {
            TSVector result;
            TSVector.Cross(ref value1, ref value2, out result);
            return result;
        }

        public static FP operator *(TSVector value1, TSVector value2) => TSVector.Dot(ref value1, ref value2);

        public static TSVector operator *(TSVector value1, FP value2)
        {
            TSVector result;
            TSVector.Multiply(ref value1, value2, out result);
            return result;
        }

        public static TSVector operator *(FP value1, TSVector value2)
        {
            TSVector result;
            TSVector.Multiply(ref value2, value1, out result);
            return result;
        }

        public static TSVector operator -(TSVector value1, TSVector value2)
        {
            TSVector result;
            TSVector.Subtract(ref value1, ref value2, out result);
            return result;
        }

        public static TSVector operator +(TSVector value1, TSVector value2)
        {
            TSVector result;
            TSVector.Add(ref value1, ref value2, out result);
            return result;
        }

        public static TSVector operator /(TSVector value1, FP value2)
        {
            TSVector result;
            TSVector.Divide(ref value1, value2, out result);
            return result;
        }

        public TSVector2 ToTSVector2() => new TSVector2(this.x, this.y);

        public TSVector4 ToTSVector4() => new TSVector4(this.x, this.y, this.z, FP.One);

        public static TSVector RotateTowards(TSVector from, TSVector to, FP maxDegree) =>
            TSQuaternion.RotateTowards(TSQuaternion.LookRotation(from), TSQuaternion.LookRotation(to), maxDegree) * TSVector.forward;

        public static TSVector Reflect(TSVector inDirection, TSVector inNormal) => -2 * TSVector.Dot(inNormal, inDirection) * inNormal + inDirection;
    }
}