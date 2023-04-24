namespace TMath
{
    public struct TSMatrix
    {
        public FP M11;
        public FP M12;
        public FP M13;
        public FP M21;
        public FP M22;
        public FP M23;
        public FP M31;
        public FP M32;
        public FP M33;
        internal static TSMatrix InternalIdentity;
        public static readonly TSMatrix Identity;
        public static readonly TSMatrix Zero = new TSMatrix();

        static TSMatrix()
        {
            TSMatrix.Identity = new TSMatrix();
            TSMatrix.Identity.M11 = FP.One;
            TSMatrix.Identity.M22 = FP.One;
            TSMatrix.Identity.M33 = FP.One;
            TSMatrix.InternalIdentity = TSMatrix.Identity;
        }

        public TSVector eulerAngles => new TSVector()
        {
            x = TSMath.Atan2(this.M32, this.M33) * FP.Rad2Deg,
            y = TSMath.Atan2(-this.M31, TSMath.Sqrt(this.M32 * this.M32 + this.M33 * this.M33)) * FP.Rad2Deg,
            z = TSMath.Atan2(this.M21, this.M11) * FP.Rad2Deg
        } * -1;

        public static TSMatrix CreateFromYawPitchRoll(FP yaw, FP pitch, FP roll)
        {
            TSQuaternion result1;
            TSQuaternion.CreateFromYawPitchRoll(yaw, pitch, roll, out result1);
            TSMatrix result2;
            TSMatrix.CreateFromQuaternion(ref result1, out result2);
            return result2;
        }

        public static TSMatrix CreateRotationX(FP radians)
        {
            FP fp1 = FP.Cos(radians);
            FP fp2 = FP.Sin(radians);
            TSMatrix rotationX;
            rotationX.M11 = FP.One;
            rotationX.M12 = FP.Zero;
            rotationX.M13 = FP.Zero;
            rotationX.M21 = FP.Zero;
            rotationX.M22 = fp1;
            rotationX.M23 = fp2;
            rotationX.M31 = FP.Zero;
            rotationX.M32 = -fp2;
            rotationX.M33 = fp1;
            return rotationX;
        }

        public static void CreateRotationX(FP radians, out TSMatrix result)
        {
            FP fp1 = FP.Cos(radians);
            FP fp2 = FP.Sin(radians);
            result.M11 = FP.One;
            result.M12 = FP.Zero;
            result.M13 = FP.Zero;
            result.M21 = FP.Zero;
            result.M22 = fp1;
            result.M23 = fp2;
            result.M31 = FP.Zero;
            result.M32 = -fp2;
            result.M33 = fp1;
        }

        public static TSMatrix CreateRotationY(FP radians)
        {
            FP fp1 = FP.Cos(radians);
            FP fp2 = FP.Sin(radians);
            TSMatrix rotationY;
            rotationY.M11 = fp1;
            rotationY.M12 = FP.Zero;
            rotationY.M13 = -fp2;
            rotationY.M21 = FP.Zero;
            rotationY.M22 = FP.One;
            rotationY.M23 = FP.Zero;
            rotationY.M31 = fp2;
            rotationY.M32 = FP.Zero;
            rotationY.M33 = fp1;
            return rotationY;
        }

        public static void CreateRotationY(FP radians, out TSMatrix result)
        {
            FP fp1 = FP.Cos(radians);
            FP fp2 = FP.Sin(radians);
            result.M11 = fp1;
            result.M12 = FP.Zero;
            result.M13 = -fp2;
            result.M21 = FP.Zero;
            result.M22 = FP.One;
            result.M23 = FP.Zero;
            result.M31 = fp2;
            result.M32 = FP.Zero;
            result.M33 = fp1;
        }

        public static TSMatrix CreateRotationZ(FP radians)
        {
            FP fp1 = FP.Cos(radians);
            FP fp2 = FP.Sin(radians);
            TSMatrix rotationZ;
            rotationZ.M11 = fp1;
            rotationZ.M12 = fp2;
            rotationZ.M13 = FP.Zero;
            rotationZ.M21 = -fp2;
            rotationZ.M22 = fp1;
            rotationZ.M23 = FP.Zero;
            rotationZ.M31 = FP.Zero;
            rotationZ.M32 = FP.Zero;
            rotationZ.M33 = FP.One;
            return rotationZ;
        }

        public static void CreateRotationZ(FP radians, out TSMatrix result)
        {
            FP fp1 = FP.Cos(radians);
            FP fp2 = FP.Sin(radians);
            result.M11 = fp1;
            result.M12 = fp2;
            result.M13 = FP.Zero;
            result.M21 = -fp2;
            result.M22 = fp1;
            result.M23 = FP.Zero;
            result.M31 = FP.Zero;
            result.M32 = FP.Zero;
            result.M33 = FP.One;
        }

        public TSMatrix(FP m11, FP m12, FP m13, FP m21, FP m22, FP m23, FP m31, FP m32, FP m33)
        {
            this.M11 = m11;
            this.M12 = m12;
            this.M13 = m13;
            this.M21 = m21;
            this.M22 = m22;
            this.M23 = m23;
            this.M31 = m31;
            this.M32 = m32;
            this.M33 = m33;
        }

        public static TSMatrix Multiply(TSMatrix matrix1, TSMatrix matrix2)
        {
            TSMatrix result;
            TSMatrix.Multiply(ref matrix1, ref matrix2, out result);
            return result;
        }

        public static void Multiply(ref TSMatrix matrix1, ref TSMatrix matrix2, out TSMatrix result)
        {
            FP fp1 = matrix1.M11 * matrix2.M11 + matrix1.M12 * matrix2.M21 + matrix1.M13 * matrix2.M31;
            FP fp2 = matrix1.M11 * matrix2.M12 + matrix1.M12 * matrix2.M22 + matrix1.M13 * matrix2.M32;
            FP fp3 = matrix1.M11 * matrix2.M13 + matrix1.M12 * matrix2.M23 + matrix1.M13 * matrix2.M33;
            FP fp4 = matrix1.M21 * matrix2.M11 + matrix1.M22 * matrix2.M21 + matrix1.M23 * matrix2.M31;
            FP fp5 = matrix1.M21 * matrix2.M12 + matrix1.M22 * matrix2.M22 + matrix1.M23 * matrix2.M32;
            FP fp6 = matrix1.M21 * matrix2.M13 + matrix1.M22 * matrix2.M23 + matrix1.M23 * matrix2.M33;
            FP fp7 = matrix1.M31 * matrix2.M11 + matrix1.M32 * matrix2.M21 + matrix1.M33 * matrix2.M31;
            FP fp8 = matrix1.M31 * matrix2.M12 + matrix1.M32 * matrix2.M22 + matrix1.M33 * matrix2.M32;
            FP fp9 = matrix1.M31 * matrix2.M13 + matrix1.M32 * matrix2.M23 + matrix1.M33 * matrix2.M33;
            result.M11 = fp1;
            result.M12 = fp2;
            result.M13 = fp3;
            result.M21 = fp4;
            result.M22 = fp5;
            result.M23 = fp6;
            result.M31 = fp7;
            result.M32 = fp8;
            result.M33 = fp9;
        }

        public static TSMatrix Add(TSMatrix matrix1, TSMatrix matrix2)
        {
            TSMatrix result;
            TSMatrix.Add(ref matrix1, ref matrix2, out result);
            return result;
        }

        public static void Add(ref TSMatrix matrix1, ref TSMatrix matrix2, out TSMatrix result)
        {
            result.M11 = matrix1.M11 + matrix2.M11;
            result.M12 = matrix1.M12 + matrix2.M12;
            result.M13 = matrix1.M13 + matrix2.M13;
            result.M21 = matrix1.M21 + matrix2.M21;
            result.M22 = matrix1.M22 + matrix2.M22;
            result.M23 = matrix1.M23 + matrix2.M23;
            result.M31 = matrix1.M31 + matrix2.M31;
            result.M32 = matrix1.M32 + matrix2.M32;
            result.M33 = matrix1.M33 + matrix2.M33;
        }

        public static TSMatrix Inverse(TSMatrix matrix)
        {
            TSMatrix result;
            TSMatrix.Inverse(ref matrix, out result);
            return result;
        }

        public FP Determinant() => this.M11 * this.M22 * this.M33 + this.M12 * this.M23 * this.M31 + this.M13 * this.M21 * this.M32 - this.M31 * this.M22 * this.M13 -
                                   this.M32 * this.M23 * this.M11 - this.M33 * this.M21 * this.M12;

        public static void Invert(ref TSMatrix matrix, out TSMatrix result)
        {
            FP fp1 = 1 / matrix.Determinant();
            FP fp2 = (matrix.M22 * matrix.M33 - matrix.M23 * matrix.M32) * fp1;
            FP fp3 = (matrix.M13 * matrix.M32 - matrix.M33 * matrix.M12) * fp1;
            FP fp4 = (matrix.M12 * matrix.M23 - matrix.M22 * matrix.M13) * fp1;
            FP fp5 = (matrix.M23 * matrix.M31 - matrix.M21 * matrix.M33) * fp1;
            FP fp6 = (matrix.M11 * matrix.M33 - matrix.M13 * matrix.M31) * fp1;
            FP fp7 = (matrix.M13 * matrix.M21 - matrix.M11 * matrix.M23) * fp1;
            FP fp8 = (matrix.M21 * matrix.M32 - matrix.M22 * matrix.M31) * fp1;
            FP fp9 = (matrix.M12 * matrix.M31 - matrix.M11 * matrix.M32) * fp1;
            FP fp10 = (matrix.M11 * matrix.M22 - matrix.M12 * matrix.M21) * fp1;
            result.M11 = fp2;
            result.M12 = fp3;
            result.M13 = fp4;
            result.M21 = fp5;
            result.M22 = fp6;
            result.M23 = fp7;
            result.M31 = fp8;
            result.M32 = fp9;
            result.M33 = fp10;
        }

        public static void Inverse(ref TSMatrix matrix, out TSMatrix result)
        {
            FP fp1 = 1024 * matrix.M11 * matrix.M22 * matrix.M33 - 1024 * matrix.M11 * matrix.M23 * matrix.M32 - 1024 * matrix.M12 * matrix.M21 * matrix.M33 +
                1024 * matrix.M12 * matrix.M23 * matrix.M31 + 1024 * matrix.M13 * matrix.M21 * matrix.M32 - 1024 * matrix.M13 * matrix.M22 * matrix.M31;
            FP fp2 = 1024 * matrix.M22 * matrix.M33 - 1024 * matrix.M23 * matrix.M32;
            FP fp3 = 1024 * matrix.M13 * matrix.M32 - 1024 * matrix.M12 * matrix.M33;
            FP fp4 = 1024 * matrix.M12 * matrix.M23 - 1024 * matrix.M22 * matrix.M13;
            FP fp5 = 1024 * matrix.M23 * matrix.M31 - 1024 * matrix.M33 * matrix.M21;
            FP fp6 = 1024 * matrix.M11 * matrix.M33 - 1024 * matrix.M31 * matrix.M13;
            FP fp7 = 1024 * matrix.M13 * matrix.M21 - 1024 * matrix.M23 * matrix.M11;
            FP fp8 = 1024 * matrix.M21 * matrix.M32 - 1024 * matrix.M31 * matrix.M22;
            FP fp9 = 1024 * matrix.M12 * matrix.M31 - 1024 * matrix.M32 * matrix.M11;
            FP fp10 = 1024 * matrix.M11 * matrix.M22 - 1024 * matrix.M21 * matrix.M12;
            if (fp1 == 0)
            {
                result.M11 = FP.PositiveInfinity;
                result.M12 = FP.PositiveInfinity;
                result.M13 = FP.PositiveInfinity;
                result.M21 = FP.PositiveInfinity;
                result.M22 = FP.PositiveInfinity;
                result.M23 = FP.PositiveInfinity;
                result.M31 = FP.PositiveInfinity;
                result.M32 = FP.PositiveInfinity;
                result.M33 = FP.PositiveInfinity;
            }
            else
            {
                result.M11 = fp2 / fp1;
                result.M12 = fp3 / fp1;
                result.M13 = fp4 / fp1;
                result.M21 = fp5 / fp1;
                result.M22 = fp6 / fp1;
                result.M23 = fp7 / fp1;
                result.M31 = fp8 / fp1;
                result.M32 = fp9 / fp1;
                result.M33 = fp10 / fp1;
            }
        }

        public static TSMatrix Multiply(TSMatrix matrix1, FP scaleFactor)
        {
            TSMatrix result;
            TSMatrix.Multiply(ref matrix1, scaleFactor, out result);
            return result;
        }

        public static void Multiply(ref TSMatrix matrix1, FP scaleFactor, out TSMatrix result)
        {
            FP fp = scaleFactor;
            result.M11 = matrix1.M11 * fp;
            result.M12 = matrix1.M12 * fp;
            result.M13 = matrix1.M13 * fp;
            result.M21 = matrix1.M21 * fp;
            result.M22 = matrix1.M22 * fp;
            result.M23 = matrix1.M23 * fp;
            result.M31 = matrix1.M31 * fp;
            result.M32 = matrix1.M32 * fp;
            result.M33 = matrix1.M33 * fp;
        }

        public static TSMatrix CreateFromLookAt(TSVector position, TSVector target)
        {
            TSMatrix result;
            TSMatrix.LookAt(target - position, TSVector.up, out result);
            return result;
        }

        public static TSMatrix LookAt(TSVector forward, TSVector upwards)
        {
            TSMatrix result;
            TSMatrix.LookAt(forward, upwards, out result);
            return result;
        }

        public static void LookAt(TSVector forward, TSVector upwards, out TSMatrix result)
        {
            TSVector tsVector1 = forward;
            tsVector1.Normalize();
            TSVector vector2 = TSVector.Cross(upwards, tsVector1);
            vector2.Normalize();
            TSVector tsVector2 = TSVector.Cross(tsVector1, vector2);
            result.M11 = vector2.x;
            result.M21 = tsVector2.x;
            result.M31 = tsVector1.x;
            result.M12 = vector2.y;
            result.M22 = tsVector2.y;
            result.M32 = tsVector1.y;
            result.M13 = vector2.z;
            result.M23 = tsVector2.z;
            result.M33 = tsVector1.z;
        }

        public static TSMatrix CreateFromQuaternion(TSQuaternion quaternion)
        {
            TSMatrix result;
            TSMatrix.CreateFromQuaternion(ref quaternion, out result);
            return result;
        }

        public static void CreateFromQuaternion(ref TSQuaternion quaternion, out TSMatrix result)
        {
            FP fp1 = quaternion.x * quaternion.x;
            FP fp2 = quaternion.y * quaternion.y;
            FP fp3 = quaternion.z * quaternion.z;
            FP fp4 = quaternion.x * quaternion.y;
            FP fp5 = quaternion.z * quaternion.w;
            FP fp6 = quaternion.z * quaternion.x;
            FP fp7 = quaternion.y * quaternion.w;
            FP fp8 = quaternion.y * quaternion.z;
            FP fp9 = quaternion.x * quaternion.w;
            result.M11 = FP.One - 2 * (fp2 + fp3);
            result.M12 = 2 * (fp4 + fp5);
            result.M13 = 2 * (fp6 - fp7);
            result.M21 = 2 * (fp4 - fp5);
            result.M22 = FP.One - 2 * (fp3 + fp1);
            result.M23 = 2 * (fp8 + fp9);
            result.M31 = 2 * (fp6 + fp7);
            result.M32 = 2 * (fp8 - fp9);
            result.M33 = FP.One - 2 * (fp2 + fp1);
        }

        public static TSMatrix Transpose(TSMatrix matrix)
        {
            TSMatrix result;
            TSMatrix.Transpose(ref matrix, out result);
            return result;
        }

        public static void Transpose(ref TSMatrix matrix, out TSMatrix result)
        {
            result.M11 = matrix.M11;
            result.M12 = matrix.M21;
            result.M13 = matrix.M31;
            result.M21 = matrix.M12;
            result.M22 = matrix.M22;
            result.M23 = matrix.M32;
            result.M31 = matrix.M13;
            result.M32 = matrix.M23;
            result.M33 = matrix.M33;
        }

        public static TSMatrix operator *(TSMatrix value1, TSMatrix value2)
        {
            TSMatrix result;
            TSMatrix.Multiply(ref value1, ref value2, out result);
            return result;
        }

        public FP Trace() => this.M11 + this.M22 + this.M33;

        public static TSMatrix operator +(TSMatrix value1, TSMatrix value2)
        {
            TSMatrix result;
            TSMatrix.Add(ref value1, ref value2, out result);
            return result;
        }

        public static TSMatrix operator -(TSMatrix value1, TSMatrix value2)
        {
            TSMatrix.Multiply(ref value2, -FP.One, out value2);
            TSMatrix result;
            TSMatrix.Add(ref value1, ref value2, out result);
            return result;
        }

        public static bool operator ==(TSMatrix value1, TSMatrix value2) => value1.M11 == value2.M11 && value1.M12 == value2.M12 && value1.M13 == value2.M13 &&
                                                                            value1.M21 == value2.M21 && value1.M22 == value2.M22 && value1.M23 == value2.M23 &&
                                                                            value1.M31 == value2.M31 && value1.M32 == value2.M32 && value1.M33 == value2.M33;

        public static bool operator !=(TSMatrix value1, TSMatrix value2) => value1.M11 != value2.M11 || value1.M12 != value2.M12 || value1.M13 != value2.M13 ||
                                                                            value1.M21 != value2.M21 || value1.M22 != value2.M22 || value1.M23 != value2.M23 ||
                                                                            value1.M31 != value2.M31 || value1.M32 != value2.M32 || value1.M33 != value2.M33;

        public override bool Equals(object obj) => obj is TSMatrix tsMatrix && this.M11 == tsMatrix.M11 && this.M12 == tsMatrix.M12 && this.M13 == tsMatrix.M13 &&
                                                   this.M21 == tsMatrix.M21 && this.M22 == tsMatrix.M22 && this.M23 == tsMatrix.M23 && this.M31 == tsMatrix.M31 &&
                                                   this.M32 == tsMatrix.M32 && this.M33 == tsMatrix.M33;

        public override int GetHashCode() => this.M11.GetHashCode() ^ this.M12.GetHashCode() ^ this.M13.GetHashCode() ^ this.M21.GetHashCode() ^ this.M22.GetHashCode() ^
                                             this.M23.GetHashCode() ^ this.M31.GetHashCode() ^ this.M32.GetHashCode() ^ this.M33.GetHashCode();

        public static void CreateFromAxisAngle(ref TSVector axis, FP angle, out TSMatrix result)
        {
            FP x = axis.x;
            FP y = axis.y;
            FP z = axis.z;
            FP fp1 = FP.Sin(angle);
            FP fp2 = FP.Cos(angle);
            FP fp3 = x * x;
            FP fp4 = y * y;
            FP fp5 = z * z;
            FP fp6 = x * y;
            FP fp7 = x * z;
            FP fp8 = y * z;
            result.M11 = fp3 + fp2 * (FP.One - fp3);
            result.M12 = fp6 - fp2 * fp6 + fp1 * z;
            result.M13 = fp7 - fp2 * fp7 - fp1 * y;
            result.M21 = fp6 - fp2 * fp6 - fp1 * z;
            result.M22 = fp4 + fp2 * (FP.One - fp4);
            result.M23 = fp8 - fp2 * fp8 + fp1 * x;
            result.M31 = fp7 - fp2 * fp7 + fp1 * y;
            result.M32 = fp8 - fp2 * fp8 - fp1 * x;
            result.M33 = fp5 + fp2 * (FP.One - fp5);
        }

        public static TSMatrix AngleAxis(FP angle, TSVector axis)
        {
            TSMatrix result;
            TSMatrix.CreateFromAxisAngle(ref axis, angle, out result);
            return result;
        }

        public override string ToString() => string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}", this.M11.RawValue, this.M12.RawValue, this.M13.RawValue,
            this.M21.RawValue, this.M22.RawValue, this.M23.RawValue, this.M31.RawValue, this.M32.RawValue, this.M33.RawValue);
    }
}