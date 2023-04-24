using System;

namespace TMath
{
    [Serializable]
    public struct TSQuaternion
    {
        public FP x;
        public FP y;
        public FP z;
        public FP w;
        public static readonly TSQuaternion identity;
        public static TSQuaternion Left90Rotate = TSQuaternion.Euler(0, -90, 0);
        public static TSQuaternion Right90Rotate = TSQuaternion.Euler(0, 90, 0);

        static TSQuaternion() => TSQuaternion.identity = new TSQuaternion(0, 0, 0, 1);

        public TSQuaternion(FP x, FP y, FP z, FP w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public void Set(FP new_x, FP new_y, FP new_z, FP new_w)
        {
            this.x = new_x;
            this.y = new_y;
            this.z = new_z;
            this.w = new_w;
        }

        public void SetFromToRotation(TSVector fromDirection, TSVector toDirection)
        {
            TSQuaternion rotation = TSQuaternion.FromToRotation(fromDirection, toDirection);
            this.Set(rotation.x, rotation.y, rotation.z, rotation.w);
        }

        public TSVector eulerAngles
        {
            get
            {
                TSVector tsVector = new TSVector();
                FP fp1 = this.y * this.y;
                FP x1 = -2 * (fp1 + this.z * this.z) + FP.One;
                FP y1 = 2 * (this.x * this.y - this.w * this.z);
                FP fp2 = -2 * (this.x * this.z + this.w * this.y);
                FP y2 = 2 * (this.y * this.z - this.w * this.x);
                FP x2 = -2 * (this.x * this.x + fp1) + FP.One;
                FP fp3 = fp2 > FP.One ? FP.One : fp2;
                FP fp4 = fp3 < -1 ? -1 : fp3;
                tsVector.x = FP.Atan2(y2, x2) * FP.Rad2Deg;
                tsVector.y = FP.Asin(fp4) * FP.Rad2Deg;
                tsVector.z = FP.Atan2(y1, x1) * FP.Rad2Deg;
                return tsVector * -1;
            }
        }

        public static FP Angle(TSQuaternion a, TSQuaternion b)
        {
            TSQuaternion tsQuaternion = TSQuaternion.Inverse(a);
            FP fp = FP.Acos((b * tsQuaternion).w) * 2 * FP.Rad2Deg;
            if (fp > 180)
                fp = 360 - fp;
            return fp;
        }

        public static TSQuaternion Add(TSQuaternion quaternion1, TSQuaternion quaternion2)
        {
            TSQuaternion result;
            TSQuaternion.Add(ref quaternion1, ref quaternion2, out result);
            return result;
        }

        public static TSQuaternion LookRotation(TSVector forward) => TSQuaternion.CreateFromMatrix(TSMatrix.LookAt(forward, TSVector.up));

        public static TSQuaternion LookRotation(TSVector forward, TSVector upwards) => TSQuaternion.CreateFromMatrix(TSMatrix.LookAt(forward, upwards));

        public static TSQuaternion Slerp(TSQuaternion from, TSQuaternion to, FP t)
        {
            t = TSMath.Clamp(t, 0, 1);
            FP x1 = TSQuaternion.Dot(from, to);
            if (x1 < FP.Zero)
            {
                to = TSQuaternion.Multiply(to, -1);
                x1 = -x1;
            }

            FP x2 = FP.Acos(x1);
            return TSQuaternion.Multiply(TSQuaternion.Multiply(from, FP.Sin((1 - t) * x2)) + TSQuaternion.Multiply(to, FP.Sin(t * x2)), 1 / FP.Sin(x2));
        }

        public static TSQuaternion RotateTowards(
            TSQuaternion from,
            TSQuaternion to,
            FP maxDegreesDelta)
        {
            FP x1 = TSQuaternion.Dot(from, to);
            if (x1 < FP.Zero)
            {
                to = TSQuaternion.Multiply(to, -1);
                x1 = -x1;
            }

            FP x2 = FP.Acos(x1);
            FP fp = x2 * 2;
            maxDegreesDelta *= FP.Deg2Rad;
            if (maxDegreesDelta >= fp)
                return to;
            maxDegreesDelta /= fp;
            return TSQuaternion.Multiply(TSQuaternion.Multiply(from, FP.Sin((1 - maxDegreesDelta) * x2)) + TSQuaternion.Multiply(to, FP.Sin(maxDegreesDelta * x2)), 1 / FP.Sin(x2));
        }

        public static TSQuaternion Euler(FP x, FP y, FP z)
        {
            x *= FP.Deg2Rad;
            y *= FP.Deg2Rad;
            z *= FP.Deg2Rad;
            TSQuaternion result;
            TSQuaternion.CreateFromYawPitchRoll(y, x, z, out result);
            return result;
        }

        public static TSQuaternion Euler(TSVector eulerAngles) => TSQuaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);

        public static TSQuaternion AngleAxis(FP angle, TSVector axis)
        {
            axis *= FP.Deg2Rad;
            axis.Normalize();
            FP x = angle * FP.Deg2Rad * FP.Half;
            FP fp = FP.Sin(x);
            TSQuaternion tsQuaternion;
            tsQuaternion.x = axis.x * fp;
            tsQuaternion.y = axis.y * fp;
            tsQuaternion.z = axis.z * fp;
            tsQuaternion.w = FP.Cos(x);
            return tsQuaternion;
        }

        public static void CreateFromYawPitchRoll(FP yaw, FP pitch, FP roll, out TSQuaternion result)
        {
            FP x1 = roll * FP.Half;
            FP fp1 = FP.Sin(x1);
            FP fp2 = FP.Cos(x1);
            FP x2 = pitch * FP.Half;
            FP fp3 = FP.Sin(x2);
            FP fp4 = FP.Cos(x2);
            FP x3 = yaw * FP.Half;
            FP fp5 = FP.Sin(x3);
            FP fp6 = FP.Cos(x3);
            result.x = fp6 * fp3 * fp2 + fp5 * fp4 * fp1;
            result.y = fp5 * fp4 * fp2 - fp6 * fp3 * fp1;
            result.z = fp6 * fp4 * fp1 - fp5 * fp3 * fp2;
            result.w = fp6 * fp4 * fp2 + fp5 * fp3 * fp1;
        }

        public static void Add(
            ref TSQuaternion quaternion1,
            ref TSQuaternion quaternion2,
            out TSQuaternion result)
        {
            result.x = quaternion1.x + quaternion2.x;
            result.y = quaternion1.y + quaternion2.y;
            result.z = quaternion1.z + quaternion2.z;
            result.w = quaternion1.w + quaternion2.w;
        }

        public static TSQuaternion Conjugate(TSQuaternion value)
        {
            TSQuaternion tsQuaternion;
            tsQuaternion.x = -value.x;
            tsQuaternion.y = -value.y;
            tsQuaternion.z = -value.z;
            tsQuaternion.w = value.w;
            return tsQuaternion;
        }

        public static FP Dot(TSQuaternion a, TSQuaternion b) => a.w * b.w + a.x * b.x + a.y * b.y + a.z * b.z;

        public static TSQuaternion Inverse(TSQuaternion rotation)
        {
            FP scaleFactor = FP.One / (rotation.x * rotation.x + rotation.y * rotation.y + rotation.z * rotation.z + rotation.w * rotation.w);
            return TSQuaternion.Multiply(TSQuaternion.Conjugate(rotation), scaleFactor);
        }

        public static TSQuaternion FromToRotation(TSVector fromVector, TSVector toVector)
        {
            TSVector tsVector = TSVector.Cross(fromVector, toVector);
            TSQuaternion rotation = new TSQuaternion(tsVector.x, tsVector.y, tsVector.z, TSVector.Dot(fromVector, toVector));
            rotation.w += FP.Sqrt(fromVector.sqrMagnitude * toVector.sqrMagnitude);
            rotation.Normalize();
            return rotation;
        }

        public static TSQuaternion Lerp(TSQuaternion a, TSQuaternion b, FP t)
        {
            t = TSMath.Clamp(t, FP.Zero, FP.One);
            return TSQuaternion.LerpUnclamped(a, b, t);
        }

        public static TSQuaternion LerpUnclamped(TSQuaternion a, TSQuaternion b, FP t)
        {
            TSQuaternion tsQuaternion = TSQuaternion.Multiply(a, 1 - t) + TSQuaternion.Multiply(b, t);
            tsQuaternion.Normalize();
            return tsQuaternion;
        }

        public static TSQuaternion Subtract(
            TSQuaternion quaternion1,
            TSQuaternion quaternion2)
        {
            TSQuaternion result;
            TSQuaternion.Subtract(ref quaternion1, ref quaternion2, out result);
            return result;
        }

        public static void Subtract(
            ref TSQuaternion quaternion1,
            ref TSQuaternion quaternion2,
            out TSQuaternion result)
        {
            result.x = quaternion1.x - quaternion2.x;
            result.y = quaternion1.y - quaternion2.y;
            result.z = quaternion1.z - quaternion2.z;
            result.w = quaternion1.w - quaternion2.w;
        }

        public static TSQuaternion Multiply(
            TSQuaternion quaternion1,
            TSQuaternion quaternion2)
        {
            TSQuaternion result;
            TSQuaternion.Multiply(ref quaternion1, ref quaternion2, out result);
            return result;
        }

        public static void Multiply(
            ref TSQuaternion quaternion1,
            ref TSQuaternion quaternion2,
            out TSQuaternion result)
        {
            FP x1 = quaternion1.x;
            FP y1 = quaternion1.y;
            FP z1 = quaternion1.z;
            FP w1 = quaternion1.w;
            FP x2 = quaternion2.x;
            FP y2 = quaternion2.y;
            FP z2 = quaternion2.z;
            FP w2 = quaternion2.w;
            FP fp1 = y1 * z2 - z1 * y2;
            FP fp2 = z1 * x2 - x1 * z2;
            FP fp3 = x1 * y2 - y1 * x2;
            FP fp4 = x1 * x2 + y1 * y2 + z1 * z2;
            result.x = x1 * w2 + x2 * w1 + fp1;
            result.y = y1 * w2 + y2 * w1 + fp2;
            result.z = z1 * w2 + z2 * w1 + fp3;
            result.w = w1 * w2 - fp4;
        }

        public static TSQuaternion Multiply(TSQuaternion quaternion1, FP scaleFactor)
        {
            TSQuaternion result;
            TSQuaternion.Multiply(ref quaternion1, scaleFactor, out result);
            return result;
        }

        public static void Multiply(
            ref TSQuaternion quaternion1,
            FP scaleFactor,
            out TSQuaternion result)
        {
            result.x = quaternion1.x * scaleFactor;
            result.y = quaternion1.y * scaleFactor;
            result.z = quaternion1.z * scaleFactor;
            result.w = quaternion1.w * scaleFactor;
        }

        public void Normalize()
        {
            FP fp = 1 / FP.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z + this.w * this.w);
            this.x *= fp;
            this.y *= fp;
            this.z *= fp;
            this.w *= fp;
        }

        public static TSQuaternion CreateFromMatrix(TSMatrix matrix)
        {
            TSQuaternion result;
            TSQuaternion.CreateFromMatrix(ref matrix, out result);
            return result;
        }

        public static void CreateFromMatrix(ref TSMatrix matrix, out TSQuaternion result)
        {
            FP fp1 = matrix.M11 + matrix.M22 + matrix.M33;
            if (fp1 > FP.Zero)
            {
                FP fp2 = FP.Sqrt(fp1 + FP.One);
                result.w = fp2 * FP.Half;
                FP fp3 = FP.Half / fp2;
                result.x = (matrix.M23 - matrix.M32) * fp3;
                result.y = (matrix.M31 - matrix.M13) * fp3;
                result.z = (matrix.M12 - matrix.M21) * fp3;
            }
            else if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
            {
                FP fp4 = FP.Sqrt(FP.One + matrix.M11 - matrix.M22 - matrix.M33);
                FP fp5 = FP.Half / fp4;
                result.x = FP.Half * fp4;
                result.y = (matrix.M12 + matrix.M21) * fp5;
                result.z = (matrix.M13 + matrix.M31) * fp5;
                result.w = (matrix.M23 - matrix.M32) * fp5;
            }
            else if (matrix.M22 > matrix.M33)
            {
                FP fp6 = FP.Sqrt(FP.One + matrix.M22 - matrix.M11 - matrix.M33);
                FP fp7 = FP.Half / fp6;
                result.x = (matrix.M21 + matrix.M12) * fp7;
                result.y = FP.Half * fp6;
                result.z = (matrix.M32 + matrix.M23) * fp7;
                result.w = (matrix.M31 - matrix.M13) * fp7;
            }
            else
            {
                FP fp8 = FP.Sqrt(FP.One + matrix.M33 - matrix.M11 - matrix.M22);
                FP fp9 = FP.Half / fp8;
                result.x = (matrix.M31 + matrix.M13) * fp9;
                result.y = (matrix.M32 + matrix.M23) * fp9;
                result.z = FP.Half * fp8;
                result.w = (matrix.M12 - matrix.M21) * fp9;
            }
        }

        public static TSQuaternion operator *(TSQuaternion value1, TSQuaternion value2)
        {
            TSQuaternion result;
            TSQuaternion.Multiply(ref value1, ref value2, out result);
            return result;
        }

        public static TSQuaternion operator +(TSQuaternion value1, TSQuaternion value2)
        {
            TSQuaternion result;
            TSQuaternion.Add(ref value1, ref value2, out result);
            return result;
        }

        public static TSQuaternion operator -(TSQuaternion value1, TSQuaternion value2)
        {
            TSQuaternion result;
            TSQuaternion.Subtract(ref value1, ref value2, out result);
            return result;
        }

        public static TSVector operator *(TSQuaternion quat, TSVector vec)
        {
            FP fp1 = quat.x * 2;
            FP fp2 = quat.y * 2;
            FP fp3 = quat.z * 2;
            FP fp4 = quat.x * fp1;
            FP fp5 = quat.y * fp2;
            FP fp6 = quat.z * fp3;
            FP fp7 = quat.x * fp2;
            FP fp8 = quat.x * fp3;
            FP fp9 = quat.y * fp3;
            FP fp10 = quat.w * fp1;
            FP fp11 = quat.w * fp2;
            FP fp12 = quat.w * fp3;
            TSVector tsVector;
            tsVector.x = (FP.One - (fp5 + fp6)) * vec.x + (fp7 - fp12) * vec.y + (fp8 + fp11) * vec.z;
            tsVector.y = (fp7 + fp12) * vec.x + (FP.One - (fp4 + fp6)) * vec.y + (fp9 - fp10) * vec.z;
            tsVector.z = (fp8 - fp11) * vec.x + (fp9 + fp10) * vec.y + (FP.One - (fp4 + fp5)) * vec.z;
            return tsVector;
        }

        public override string ToString() => string.Format("({0:f1}, {1:f1}, {2:f1}, {3:f1})", this.x.AsFloat(), this.y.AsFloat(), this.z.AsFloat(),
            this.w.AsFloat());
    }
}