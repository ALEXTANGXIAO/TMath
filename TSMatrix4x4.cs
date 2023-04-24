namespace TMath
{
    public struct TSMatrix4x4
    {
        public FP M11;
        public FP M12;
        public FP M13;
        public FP M14;
        public FP M21;
        public FP M22;
        public FP M23;
        public FP M24;
        public FP M31;
        public FP M32;
        public FP M33;
        public FP M34;
        public FP M41;
        public FP M42;
        public FP M43;
        public FP M44;
        internal static TSMatrix4x4 InternalIdentity;
        public static readonly TSMatrix4x4 Identity;
        public static readonly TSMatrix4x4 Zero = new TSMatrix4x4();

        static TSMatrix4x4()
        {
            TSMatrix4x4.Identity = new TSMatrix4x4();
            TSMatrix4x4.Identity.M11 = FP.One;
            TSMatrix4x4.Identity.M22 = FP.One;
            TSMatrix4x4.Identity.M33 = FP.One;
            TSMatrix4x4.Identity.M44 = FP.One;
            TSMatrix4x4.InternalIdentity = TSMatrix4x4.Identity;
        }

        public TSMatrix4x4(
            FP m11,
            FP m12,
            FP m13,
            FP m14,
            FP m21,
            FP m22,
            FP m23,
            FP m24,
            FP m31,
            FP m32,
            FP m33,
            FP m34,
            FP m41,
            FP m42,
            FP m43,
            FP m44)
        {
            this.M11 = m11;
            this.M12 = m12;
            this.M13 = m13;
            this.M14 = m14;
            this.M21 = m21;
            this.M22 = m22;
            this.M23 = m23;
            this.M24 = m24;
            this.M31 = m31;
            this.M32 = m32;
            this.M33 = m33;
            this.M34 = m34;
            this.M41 = m41;
            this.M42 = m42;
            this.M43 = m43;
            this.M44 = m44;
        }

        public static TSMatrix4x4 Multiply(TSMatrix4x4 matrix1, TSMatrix4x4 matrix2)
        {
            TSMatrix4x4 result;
            TSMatrix4x4.Multiply(ref matrix1, ref matrix2, out result);
            return result;
        }

        public static void Multiply(
            ref TSMatrix4x4 matrix1,
            ref TSMatrix4x4 matrix2,
            out TSMatrix4x4 result)
        {
            result.M11 = matrix1.M11 * matrix2.M11 + matrix1.M12 * matrix2.M21 + matrix1.M13 * matrix2.M31 + matrix1.M14 * matrix2.M41;
            result.M12 = matrix1.M11 * matrix2.M12 + matrix1.M12 * matrix2.M22 + matrix1.M13 * matrix2.M32 + matrix1.M14 * matrix2.M42;
            result.M13 = matrix1.M11 * matrix2.M13 + matrix1.M12 * matrix2.M23 + matrix1.M13 * matrix2.M33 + matrix1.M14 * matrix2.M43;
            result.M14 = matrix1.M11 * matrix2.M14 + matrix1.M12 * matrix2.M24 + matrix1.M13 * matrix2.M34 + matrix1.M14 * matrix2.M44;
            result.M21 = matrix1.M21 * matrix2.M11 + matrix1.M22 * matrix2.M21 + matrix1.M23 * matrix2.M31 + matrix1.M24 * matrix2.M41;
            result.M22 = matrix1.M21 * matrix2.M12 + matrix1.M22 * matrix2.M22 + matrix1.M23 * matrix2.M32 + matrix1.M24 * matrix2.M42;
            result.M23 = matrix1.M21 * matrix2.M13 + matrix1.M22 * matrix2.M23 + matrix1.M23 * matrix2.M33 + matrix1.M24 * matrix2.M43;
            result.M24 = matrix1.M21 * matrix2.M14 + matrix1.M22 * matrix2.M24 + matrix1.M23 * matrix2.M34 + matrix1.M24 * matrix2.M44;
            result.M31 = matrix1.M31 * matrix2.M11 + matrix1.M32 * matrix2.M21 + matrix1.M33 * matrix2.M31 + matrix1.M34 * matrix2.M41;
            result.M32 = matrix1.M31 * matrix2.M12 + matrix1.M32 * matrix2.M22 + matrix1.M33 * matrix2.M32 + matrix1.M34 * matrix2.M42;
            result.M33 = matrix1.M31 * matrix2.M13 + matrix1.M32 * matrix2.M23 + matrix1.M33 * matrix2.M33 + matrix1.M34 * matrix2.M43;
            result.M34 = matrix1.M31 * matrix2.M14 + matrix1.M32 * matrix2.M24 + matrix1.M33 * matrix2.M34 + matrix1.M34 * matrix2.M44;
            result.M41 = matrix1.M41 * matrix2.M11 + matrix1.M42 * matrix2.M21 + matrix1.M43 * matrix2.M31 + matrix1.M44 * matrix2.M41;
            result.M42 = matrix1.M41 * matrix2.M12 + matrix1.M42 * matrix2.M22 + matrix1.M43 * matrix2.M32 + matrix1.M44 * matrix2.M42;
            result.M43 = matrix1.M41 * matrix2.M13 + matrix1.M42 * matrix2.M23 + matrix1.M43 * matrix2.M33 + matrix1.M44 * matrix2.M43;
            result.M44 = matrix1.M41 * matrix2.M14 + matrix1.M42 * matrix2.M24 + matrix1.M43 * matrix2.M34 + matrix1.M44 * matrix2.M44;
        }

        public static TSMatrix4x4 Add(TSMatrix4x4 matrix1, TSMatrix4x4 matrix2)
        {
            TSMatrix4x4 result;
            TSMatrix4x4.Add(ref matrix1, ref matrix2, out result);
            return result;
        }

        public static void Add(
            ref TSMatrix4x4 matrix1,
            ref TSMatrix4x4 matrix2,
            out TSMatrix4x4 result)
        {
            result.M11 = matrix1.M11 + matrix2.M11;
            result.M12 = matrix1.M12 + matrix2.M12;
            result.M13 = matrix1.M13 + matrix2.M13;
            result.M14 = matrix1.M14 + matrix2.M14;
            result.M21 = matrix1.M21 + matrix2.M21;
            result.M22 = matrix1.M22 + matrix2.M22;
            result.M23 = matrix1.M23 + matrix2.M23;
            result.M24 = matrix1.M24 + matrix2.M24;
            result.M31 = matrix1.M31 + matrix2.M31;
            result.M32 = matrix1.M32 + matrix2.M32;
            result.M33 = matrix1.M33 + matrix2.M33;
            result.M34 = matrix1.M34 + matrix2.M34;
            result.M41 = matrix1.M41 + matrix2.M41;
            result.M42 = matrix1.M42 + matrix2.M42;
            result.M43 = matrix1.M43 + matrix2.M43;
            result.M44 = matrix1.M44 + matrix2.M44;
        }

        public static TSMatrix4x4 Inverse(TSMatrix4x4 matrix)
        {
            TSMatrix4x4 result;
            TSMatrix4x4.Inverse(ref matrix, out result);
            return result;
        }

        public FP determinant
        {
            get
            {
                FP m11 = this.M11;
                FP m12 = this.M12;
                FP m13 = this.M13;
                FP m14 = this.M14;
                FP m21 = this.M21;
                FP m22 = this.M22;
                FP m23 = this.M23;
                FP m24 = this.M24;
                FP m31 = this.M31;
                FP m32 = this.M32;
                FP m33 = this.M33;
                FP m34 = this.M34;
                FP m41 = this.M41;
                FP m42 = this.M42;
                FP m43 = this.M43;
                FP m44 = this.M44;
                FP fp1 = m33 * m44 - m34 * m43;
                FP fp2 = m32 * m44 - m34 * m42;
                FP fp3 = m32 * m43 - m33 * m42;
                FP fp4 = m31 * m44 - m34 * m41;
                FP fp5 = m31 * m43 - m33 * m41;
                FP fp6 = m31 * m42 - m32 * m41;
                FP fp7 = m22 * fp1 - m23 * fp2 + m24 * fp3;
                return m11 * fp7 - m12 * (m21 * fp1 - m23 * fp4 + m24 * fp5) + m13 * (m21 * fp2 - m22 * fp4 + m24 * fp6) - m14 * (m21 * fp3 - m22 * fp5 + m23 * fp6);
            }
        }

        public static void Inverse(ref TSMatrix4x4 matrix, out TSMatrix4x4 result)
        {
            FP m11 = matrix.M11;
            FP m12 = matrix.M12;
            FP m13 = matrix.M13;
            FP m14 = matrix.M14;
            FP m21 = matrix.M21;
            FP m22 = matrix.M22;
            FP m23 = matrix.M23;
            FP m24 = matrix.M24;
            FP m31 = matrix.M31;
            FP m32 = matrix.M32;
            FP m33 = matrix.M33;
            FP m34 = matrix.M34;
            FP m41 = matrix.M41;
            FP m42 = matrix.M42;
            FP m43 = matrix.M43;
            FP m44 = matrix.M44;
            FP fp1 = m33 * m44 - m34 * m43;
            FP fp2 = m32 * m44 - m34 * m42;
            FP fp3 = m32 * m43 - m33 * m42;
            FP fp4 = m31 * m44 - m34 * m41;
            FP fp5 = m31 * m43 - m33 * m41;
            FP fp6 = m31 * m42 - m32 * m41;
            FP fp7 = m22 * fp1 - m23 * fp2 + m24 * fp3;
            FP fp8 = -(m21 * fp1 - m23 * fp4 + m24 * fp5);
            FP fp9 = m21 * fp2 - m22 * fp4 + m24 * fp6;
            FP fp10 = -(m21 * fp3 - m22 * fp5 + m23 * fp6);
            FP fp11 = m11 * fp7 + m12 * fp8 + m13 * fp9 + m14 * fp10;
            if (fp11 == FP.Zero)
            {
                result.M11 = FP.PositiveInfinity;
                result.M12 = FP.PositiveInfinity;
                result.M13 = FP.PositiveInfinity;
                result.M14 = FP.PositiveInfinity;
                result.M21 = FP.PositiveInfinity;
                result.M22 = FP.PositiveInfinity;
                result.M23 = FP.PositiveInfinity;
                result.M24 = FP.PositiveInfinity;
                result.M31 = FP.PositiveInfinity;
                result.M32 = FP.PositiveInfinity;
                result.M33 = FP.PositiveInfinity;
                result.M34 = FP.PositiveInfinity;
                result.M41 = FP.PositiveInfinity;
                result.M42 = FP.PositiveInfinity;
                result.M43 = FP.PositiveInfinity;
                result.M44 = FP.PositiveInfinity;
            }
            else
            {
                FP fp12 = FP.One / fp11;
                result.M11 = fp7 * fp12;
                result.M21 = fp8 * fp12;
                result.M31 = fp9 * fp12;
                result.M41 = fp10 * fp12;
                result.M12 = -(m12 * fp1 - m13 * fp2 + m14 * fp3) * fp12;
                result.M22 = (m11 * fp1 - m13 * fp4 + m14 * fp5) * fp12;
                result.M32 = -(m11 * fp2 - m12 * fp4 + m14 * fp6) * fp12;
                result.M42 = (m11 * fp3 - m12 * fp5 + m13 * fp6) * fp12;
                FP fp13 = m23 * m44 - m24 * m43;
                FP fp14 = m22 * m44 - m24 * m42;
                FP fp15 = m22 * m43 - m23 * m42;
                FP fp16 = m21 * m44 - m24 * m41;
                FP fp17 = m21 * m43 - m23 * m41;
                FP fp18 = m21 * m42 - m22 * m41;
                result.M13 = (m12 * fp13 - m13 * fp14 + m14 * fp15) * fp12;
                result.M23 = -(m11 * fp13 - m13 * fp16 + m14 * fp17) * fp12;
                result.M33 = (m11 * fp14 - m12 * fp16 + m14 * fp18) * fp12;
                result.M43 = -(m11 * fp15 - m12 * fp17 + m13 * fp18) * fp12;
                FP fp19 = m23 * m34 - m24 * m33;
                FP fp20 = m22 * m34 - m24 * m32;
                FP fp21 = m22 * m33 - m23 * m32;
                FP fp22 = m21 * m34 - m24 * m31;
                FP fp23 = m21 * m33 - m23 * m31;
                FP fp24 = m21 * m32 - m22 * m31;
                result.M14 = -(m12 * fp19 - m13 * fp20 + m14 * fp21) * fp12;
                result.M24 = (m11 * fp19 - m13 * fp22 + m14 * fp23) * fp12;
                result.M34 = -(m11 * fp20 - m12 * fp22 + m14 * fp24) * fp12;
                result.M44 = (m11 * fp21 - m12 * fp23 + m13 * fp24) * fp12;
            }
        }

        public static TSMatrix4x4 Multiply(TSMatrix4x4 matrix1, FP scaleFactor)
        {
            TSMatrix4x4 result;
            TSMatrix4x4.Multiply(ref matrix1, scaleFactor, out result);
            return result;
        }

        public static void Multiply(ref TSMatrix4x4 matrix1, FP scaleFactor, out TSMatrix4x4 result)
        {
            FP fp = scaleFactor;
            result.M11 = matrix1.M11 * fp;
            result.M12 = matrix1.M12 * fp;
            result.M13 = matrix1.M13 * fp;
            result.M14 = matrix1.M14 * fp;
            result.M21 = matrix1.M21 * fp;
            result.M22 = matrix1.M22 * fp;
            result.M23 = matrix1.M23 * fp;
            result.M24 = matrix1.M24 * fp;
            result.M31 = matrix1.M31 * fp;
            result.M32 = matrix1.M32 * fp;
            result.M33 = matrix1.M33 * fp;
            result.M34 = matrix1.M34 * fp;
            result.M41 = matrix1.M41 * fp;
            result.M42 = matrix1.M42 * fp;
            result.M43 = matrix1.M43 * fp;
            result.M44 = matrix1.M44 * fp;
        }

        public static TSMatrix4x4 Rotate(TSQuaternion quaternion)
        {
            TSMatrix4x4 result;
            TSMatrix4x4.Rotate(ref quaternion, out result);
            return result;
        }

        public static void Rotate(ref TSQuaternion quaternion, out TSMatrix4x4 result)
        {
            FP fp1 = quaternion.x * (FP)2;
            FP fp2 = quaternion.y * (FP)2;
            FP fp3 = quaternion.z * (FP)2;
            FP fp4 = quaternion.x * fp1;
            FP fp5 = quaternion.y * fp2;
            FP fp6 = quaternion.z * fp3;
            FP fp7 = quaternion.x * fp2;
            FP fp8 = quaternion.x * fp3;
            FP fp9 = quaternion.y * fp3;
            FP fp10 = quaternion.w * fp1;
            FP fp11 = quaternion.w * fp2;
            FP fp12 = quaternion.w * fp3;
            result.M11 = FP.One - (fp5 + fp6);
            result.M21 = fp7 + fp12;
            result.M31 = fp8 - fp11;
            result.M41 = FP.Zero;
            result.M12 = fp7 - fp12;
            result.M22 = FP.One - (fp4 + fp6);
            result.M32 = fp9 + fp10;
            result.M42 = FP.Zero;
            result.M13 = fp8 + fp11;
            result.M23 = fp9 - fp10;
            result.M33 = FP.One - (fp4 + fp5);
            result.M43 = FP.Zero;
            result.M14 = FP.Zero;
            result.M24 = FP.Zero;
            result.M34 = FP.Zero;
            result.M44 = FP.One;
        }

        public static TSMatrix4x4 Transpose(TSMatrix4x4 matrix)
        {
            TSMatrix4x4 result;
            TSMatrix4x4.Transpose(ref matrix, out result);
            return result;
        }

        public static void Transpose(ref TSMatrix4x4 matrix, out TSMatrix4x4 result)
        {
            result.M11 = matrix.M11;
            result.M12 = matrix.M21;
            result.M13 = matrix.M31;
            result.M14 = matrix.M41;
            result.M21 = matrix.M12;
            result.M22 = matrix.M22;
            result.M23 = matrix.M32;
            result.M24 = matrix.M42;
            result.M31 = matrix.M13;
            result.M32 = matrix.M23;
            result.M33 = matrix.M33;
            result.M34 = matrix.M43;
            result.M41 = matrix.M14;
            result.M42 = matrix.M24;
            result.M43 = matrix.M34;
            result.M44 = matrix.M44;
        }

        public static TSMatrix4x4 operator *(TSMatrix4x4 value1, TSMatrix4x4 value2)
        {
            TSMatrix4x4 result;
            TSMatrix4x4.Multiply(ref value1, ref value2, out result);
            return result;
        }

        public FP Trace() => this.M11 + this.M22 + this.M33 + this.M44;

        public static TSMatrix4x4 operator +(TSMatrix4x4 value1, TSMatrix4x4 value2)
        {
            TSMatrix4x4 result;
            TSMatrix4x4.Add(ref value1, ref value2, out result);
            return result;
        }

        public static TSMatrix4x4 operator -(TSMatrix4x4 value)
        {
            TSMatrix4x4 tsMatrix4x4;
            tsMatrix4x4.M11 = -value.M11;
            tsMatrix4x4.M12 = -value.M12;
            tsMatrix4x4.M13 = -value.M13;
            tsMatrix4x4.M14 = -value.M14;
            tsMatrix4x4.M21 = -value.M21;
            tsMatrix4x4.M22 = -value.M22;
            tsMatrix4x4.M23 = -value.M23;
            tsMatrix4x4.M24 = -value.M24;
            tsMatrix4x4.M31 = -value.M31;
            tsMatrix4x4.M32 = -value.M32;
            tsMatrix4x4.M33 = -value.M33;
            tsMatrix4x4.M34 = -value.M34;
            tsMatrix4x4.M41 = -value.M41;
            tsMatrix4x4.M42 = -value.M42;
            tsMatrix4x4.M43 = -value.M43;
            tsMatrix4x4.M44 = -value.M44;
            return tsMatrix4x4;
        }

        public static TSMatrix4x4 operator -(TSMatrix4x4 value1, TSMatrix4x4 value2)
        {
            TSMatrix4x4.Multiply(ref value2, -FP.One, out value2);
            TSMatrix4x4 result;
            TSMatrix4x4.Add(ref value1, ref value2, out result);
            return result;
        }

        public static bool operator ==(TSMatrix4x4 value1, TSMatrix4x4 value2) => value1.M11 == value2.M11 && value1.M12 == value2.M12 && value1.M13 == value2.M13 &&
                                                                                  value1.M14 == value2.M14 && value1.M21 == value2.M21 && value1.M22 == value2.M22 &&
                                                                                  value1.M23 == value2.M23 && value1.M24 == value2.M24 && value1.M31 == value2.M31 &&
                                                                                  value1.M32 == value2.M32 && value1.M33 == value2.M33 && value1.M34 == value2.M34 &&
                                                                                  value1.M41 == value2.M41 && value1.M42 == value2.M42 && value1.M43 == value2.M43 &&
                                                                                  value1.M44 == value2.M44;

        public static bool operator !=(TSMatrix4x4 value1, TSMatrix4x4 value2) => value1.M11 != value2.M11 || value1.M12 != value2.M12 || value1.M13 != value2.M13 ||
                                                                                  value1.M14 != value2.M14 || value1.M21 != value2.M21 || value1.M22 != value2.M22 ||
                                                                                  value1.M23 != value2.M23 || value1.M24 != value2.M24 || value1.M31 != value2.M31 ||
                                                                                  value1.M32 != value2.M32 || value1.M33 != value2.M33 || value1.M34 != value2.M34 ||
                                                                                  value1.M41 != value2.M41 || value1.M42 != value2.M42 || value1.M43 != value2.M43 ||
                                                                                  value1.M44 != value2.M44;

        public override bool Equals(object obj) => obj is TSMatrix4x4 tsMatrix4x4 && this.M11 == tsMatrix4x4.M11 && this.M12 == tsMatrix4x4.M12 && this.M13 == tsMatrix4x4.M13 &&
                                                   this.M14 == tsMatrix4x4.M14 && this.M21 == tsMatrix4x4.M21 && this.M22 == tsMatrix4x4.M22 && this.M23 == tsMatrix4x4.M23 &&
                                                   this.M24 == tsMatrix4x4.M24 && this.M31 == tsMatrix4x4.M31 && this.M32 == tsMatrix4x4.M32 && this.M33 == tsMatrix4x4.M33 &&
                                                   this.M34 == tsMatrix4x4.M44 && this.M41 == tsMatrix4x4.M41 && this.M42 == tsMatrix4x4.M42 && this.M43 == tsMatrix4x4.M43 &&
                                                   this.M44 == tsMatrix4x4.M44;

        public override int GetHashCode() => this.M11.GetHashCode() ^ this.M12.GetHashCode() ^ this.M13.GetHashCode() ^ this.M14.GetHashCode() ^ this.M21.GetHashCode() ^
                                             this.M22.GetHashCode() ^ this.M23.GetHashCode() ^ this.M24.GetHashCode() ^ this.M31.GetHashCode() ^ this.M32.GetHashCode() ^
                                             this.M33.GetHashCode() ^ this.M34.GetHashCode() ^ this.M41.GetHashCode() ^ this.M42.GetHashCode() ^ this.M43.GetHashCode() ^
                                             this.M44.GetHashCode();

        public static TSMatrix4x4 Translate(FP xPosition, FP yPosition, FP zPosition)
        {
            TSMatrix4x4 tsMatrix4x4;
            tsMatrix4x4.M11 = FP.One;
            tsMatrix4x4.M12 = FP.Zero;
            tsMatrix4x4.M13 = FP.Zero;
            tsMatrix4x4.M14 = xPosition;
            tsMatrix4x4.M21 = FP.Zero;
            tsMatrix4x4.M22 = FP.One;
            tsMatrix4x4.M23 = FP.Zero;
            tsMatrix4x4.M24 = yPosition;
            tsMatrix4x4.M31 = FP.Zero;
            tsMatrix4x4.M32 = FP.Zero;
            tsMatrix4x4.M33 = FP.One;
            tsMatrix4x4.M34 = zPosition;
            tsMatrix4x4.M41 = FP.Zero;
            tsMatrix4x4.M42 = FP.Zero;
            tsMatrix4x4.M43 = FP.Zero;
            tsMatrix4x4.M44 = FP.One;
            return tsMatrix4x4;
        }

        public static TSMatrix4x4 Translate(TSVector translation) => TSMatrix4x4.Translate(translation.x, translation.y, translation.z);

        public static TSMatrix4x4 Scale(FP xScale, FP yScale, FP zScale)
        {
            TSMatrix4x4 tsMatrix4x4;
            tsMatrix4x4.M11 = xScale;
            tsMatrix4x4.M12 = FP.Zero;
            tsMatrix4x4.M13 = FP.Zero;
            tsMatrix4x4.M14 = FP.Zero;
            tsMatrix4x4.M21 = FP.Zero;
            tsMatrix4x4.M22 = yScale;
            tsMatrix4x4.M23 = FP.Zero;
            tsMatrix4x4.M24 = FP.Zero;
            tsMatrix4x4.M31 = FP.Zero;
            tsMatrix4x4.M32 = FP.Zero;
            tsMatrix4x4.M33 = zScale;
            tsMatrix4x4.M34 = FP.Zero;
            tsMatrix4x4.M41 = FP.Zero;
            tsMatrix4x4.M42 = FP.Zero;
            tsMatrix4x4.M43 = FP.Zero;
            tsMatrix4x4.M44 = FP.One;
            return tsMatrix4x4;
        }

        public static TSMatrix4x4 Scale(
            FP xScale,
            FP yScale,
            FP zScale,
            TSVector centerPoint)
        {
            FP fp1 = centerPoint.x * (FP.One - xScale);
            FP fp2 = centerPoint.y * (FP.One - yScale);
            FP fp3 = centerPoint.z * (FP.One - zScale);
            TSMatrix4x4 tsMatrix4x4;
            tsMatrix4x4.M11 = xScale;
            tsMatrix4x4.M12 = FP.Zero;
            tsMatrix4x4.M13 = FP.Zero;
            tsMatrix4x4.M14 = FP.Zero;
            tsMatrix4x4.M21 = FP.Zero;
            tsMatrix4x4.M22 = yScale;
            tsMatrix4x4.M23 = FP.Zero;
            tsMatrix4x4.M24 = FP.Zero;
            tsMatrix4x4.M31 = FP.Zero;
            tsMatrix4x4.M32 = FP.Zero;
            tsMatrix4x4.M33 = zScale;
            tsMatrix4x4.M34 = FP.Zero;
            tsMatrix4x4.M41 = fp1;
            tsMatrix4x4.M42 = fp2;
            tsMatrix4x4.M43 = fp3;
            tsMatrix4x4.M44 = FP.One;
            return tsMatrix4x4;
        }

        public static TSMatrix4x4 Scale(TSVector scales) => TSMatrix4x4.Scale(scales.x, scales.y, scales.z);

        public static TSMatrix4x4 Scale(TSVector scales, TSVector centerPoint) => TSMatrix4x4.Scale(scales.x, scales.y, scales.z, centerPoint);

        public static TSMatrix4x4 Scale(FP scale) => TSMatrix4x4.Scale(scale, scale, scale);

        public static TSMatrix4x4 Scale(FP scale, TSVector centerPoint) => TSMatrix4x4.Scale(scale, scale, scale, centerPoint);

        public static TSMatrix4x4 RotateX(FP radians)
        {
            FP fp1 = TSMath.Cos(radians);
            FP fp2 = TSMath.Sin(radians);
            TSMatrix4x4 tsMatrix4x4;
            tsMatrix4x4.M11 = FP.One;
            tsMatrix4x4.M12 = FP.Zero;
            tsMatrix4x4.M13 = FP.Zero;
            tsMatrix4x4.M14 = FP.Zero;
            tsMatrix4x4.M21 = FP.Zero;
            tsMatrix4x4.M22 = fp1;
            tsMatrix4x4.M23 = fp2;
            tsMatrix4x4.M24 = FP.Zero;
            tsMatrix4x4.M31 = FP.Zero;
            tsMatrix4x4.M32 = -fp2;
            tsMatrix4x4.M33 = fp1;
            tsMatrix4x4.M34 = FP.Zero;
            tsMatrix4x4.M41 = FP.Zero;
            tsMatrix4x4.M42 = FP.Zero;
            tsMatrix4x4.M43 = FP.Zero;
            tsMatrix4x4.M44 = FP.One;
            return tsMatrix4x4;
        }

        public static TSMatrix4x4 RotateX(FP radians, TSVector centerPoint)
        {
            FP fp1 = TSMath.Cos(radians);
            FP fp2 = TSMath.Sin(radians);
            FP fp3 = centerPoint.y * (FP.One - fp1) + centerPoint.z * fp2;
            FP fp4 = centerPoint.z * (FP.One - fp1) - centerPoint.y * fp2;
            TSMatrix4x4 tsMatrix4x4;
            tsMatrix4x4.M11 = FP.One;
            tsMatrix4x4.M12 = FP.Zero;
            tsMatrix4x4.M13 = FP.Zero;
            tsMatrix4x4.M14 = FP.Zero;
            tsMatrix4x4.M21 = FP.Zero;
            tsMatrix4x4.M22 = fp1;
            tsMatrix4x4.M23 = fp2;
            tsMatrix4x4.M24 = FP.Zero;
            tsMatrix4x4.M31 = FP.Zero;
            tsMatrix4x4.M32 = -fp2;
            tsMatrix4x4.M33 = fp1;
            tsMatrix4x4.M34 = FP.Zero;
            tsMatrix4x4.M41 = FP.Zero;
            tsMatrix4x4.M42 = fp3;
            tsMatrix4x4.M43 = fp4;
            tsMatrix4x4.M44 = FP.One;
            return tsMatrix4x4;
        }

        public static TSMatrix4x4 RotateY(FP radians)
        {
            FP fp1 = TSMath.Cos(radians);
            FP fp2 = TSMath.Sin(radians);
            TSMatrix4x4 tsMatrix4x4;
            tsMatrix4x4.M11 = fp1;
            tsMatrix4x4.M12 = FP.Zero;
            tsMatrix4x4.M13 = -fp2;
            tsMatrix4x4.M14 = FP.Zero;
            tsMatrix4x4.M21 = FP.Zero;
            tsMatrix4x4.M22 = FP.One;
            tsMatrix4x4.M23 = FP.Zero;
            tsMatrix4x4.M24 = FP.Zero;
            tsMatrix4x4.M31 = fp2;
            tsMatrix4x4.M32 = FP.Zero;
            tsMatrix4x4.M33 = fp1;
            tsMatrix4x4.M34 = FP.Zero;
            tsMatrix4x4.M41 = FP.Zero;
            tsMatrix4x4.M42 = FP.Zero;
            tsMatrix4x4.M43 = FP.Zero;
            tsMatrix4x4.M44 = FP.One;
            return tsMatrix4x4;
        }

        public static TSMatrix4x4 RotateY(FP radians, TSVector centerPoint)
        {
            FP fp1 = TSMath.Cos(radians);
            FP fp2 = TSMath.Sin(radians);
            FP fp3 = centerPoint.x * (FP.One - fp1) - centerPoint.z * fp2;
            FP fp4 = centerPoint.x * (FP.One - fp1) + centerPoint.x * fp2;
            TSMatrix4x4 tsMatrix4x4;
            tsMatrix4x4.M11 = fp1;
            tsMatrix4x4.M12 = FP.Zero;
            tsMatrix4x4.M13 = -fp2;
            tsMatrix4x4.M14 = FP.Zero;
            tsMatrix4x4.M21 = FP.Zero;
            tsMatrix4x4.M22 = FP.One;
            tsMatrix4x4.M23 = FP.Zero;
            tsMatrix4x4.M24 = FP.Zero;
            tsMatrix4x4.M31 = fp2;
            tsMatrix4x4.M32 = FP.Zero;
            tsMatrix4x4.M33 = fp1;
            tsMatrix4x4.M34 = FP.Zero;
            tsMatrix4x4.M41 = fp3;
            tsMatrix4x4.M42 = FP.Zero;
            tsMatrix4x4.M43 = fp4;
            tsMatrix4x4.M44 = FP.One;
            return tsMatrix4x4;
        }

        public static TSMatrix4x4 RotateZ(FP radians)
        {
            FP fp1 = TSMath.Cos(radians);
            FP fp2 = TSMath.Sin(radians);
            TSMatrix4x4 tsMatrix4x4;
            tsMatrix4x4.M11 = fp1;
            tsMatrix4x4.M12 = fp2;
            tsMatrix4x4.M13 = FP.Zero;
            tsMatrix4x4.M14 = FP.Zero;
            tsMatrix4x4.M21 = -fp2;
            tsMatrix4x4.M22 = fp1;
            tsMatrix4x4.M23 = FP.Zero;
            tsMatrix4x4.M24 = FP.Zero;
            tsMatrix4x4.M31 = FP.Zero;
            tsMatrix4x4.M32 = FP.Zero;
            tsMatrix4x4.M33 = FP.One;
            tsMatrix4x4.M34 = FP.Zero;
            tsMatrix4x4.M41 = FP.Zero;
            tsMatrix4x4.M42 = FP.Zero;
            tsMatrix4x4.M43 = FP.Zero;
            tsMatrix4x4.M44 = FP.One;
            return tsMatrix4x4;
        }

        public static TSMatrix4x4 RotateZ(FP radians, TSVector centerPoint)
        {
            FP fp1 = TSMath.Cos(radians);
            FP fp2 = TSMath.Sin(radians);
            FP fp3 = centerPoint.x * ((FP)1 - fp1) + centerPoint.y * fp2;
            FP fp4 = centerPoint.y * ((FP)1 - fp1) - centerPoint.x * fp2;
            TSMatrix4x4 tsMatrix4x4;
            tsMatrix4x4.M11 = fp1;
            tsMatrix4x4.M12 = fp2;
            tsMatrix4x4.M13 = FP.Zero;
            tsMatrix4x4.M14 = FP.Zero;
            tsMatrix4x4.M21 = -fp2;
            tsMatrix4x4.M22 = fp1;
            tsMatrix4x4.M23 = FP.Zero;
            tsMatrix4x4.M24 = FP.Zero;
            tsMatrix4x4.M31 = FP.Zero;
            tsMatrix4x4.M32 = FP.Zero;
            tsMatrix4x4.M33 = FP.One;
            tsMatrix4x4.M34 = FP.Zero;
            tsMatrix4x4.M41 = FP.Zero;
            tsMatrix4x4.M42 = FP.Zero;
            tsMatrix4x4.M43 = FP.Zero;
            tsMatrix4x4.M44 = FP.One;
            return tsMatrix4x4;
        }

        public static void AxisAngle(ref TSVector axis, FP angle, out TSMatrix4x4 result)
        {
            FP x = axis.x;
            FP y = axis.y;
            FP z = axis.z;
            FP fp1 = TSMath.Sin(angle);
            FP fp2 = TSMath.Cos(angle);
            FP fp3 = x * x;
            FP fp4 = y * y;
            FP fp5 = z * z;
            FP fp6 = x * y;
            FP fp7 = x * z;
            FP fp8 = y * z;
            result.M11 = fp3 + fp2 * (FP.One - fp3);
            result.M12 = fp6 - fp2 * fp6 + fp1 * z;
            result.M13 = fp7 - fp2 * fp7 - fp1 * y;
            result.M14 = FP.Zero;
            result.M21 = fp6 - fp2 * fp6 - fp1 * z;
            result.M22 = fp4 + fp2 * (FP.One - fp4);
            result.M23 = fp8 - fp2 * fp8 + fp1 * x;
            result.M24 = FP.Zero;
            result.M31 = fp7 - fp2 * fp7 + fp1 * y;
            result.M32 = fp8 - fp2 * fp8 - fp1 * x;
            result.M33 = fp5 + fp2 * (FP.One - fp5);
            result.M34 = FP.Zero;
            result.M41 = FP.Zero;
            result.M42 = FP.Zero;
            result.M43 = FP.Zero;
            result.M44 = FP.One;
        }

        public static TSMatrix4x4 AngleAxis(FP angle, TSVector axis)
        {
            TSMatrix4x4 result;
            TSMatrix4x4.AxisAngle(ref axis, angle, out result);
            return result;
        }

        public override string ToString() => string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}", this.M11.RawValue,
            this.M12.RawValue, this.M13.RawValue, this.M14.RawValue, this.M21.RawValue, this.M22.RawValue, this.M23.RawValue,
            this.M24.RawValue, this.M31.RawValue, this.M32.RawValue, this.M33.RawValue, this.M34.RawValue, this.M41.RawValue,
            this.M42.RawValue, this.M43.RawValue, this.M44.RawValue);

        public static void TRS(
            TSVector translation,
            TSQuaternion rotation,
            TSVector scale,
            out TSMatrix4x4 matrix)
        {
            matrix = TSMatrix4x4.Translate(translation) * TSMatrix4x4.Rotate(rotation) * TSMatrix4x4.Scale(scale);
        }

        public static TSMatrix4x4 TRS(
            TSVector translation,
            TSQuaternion rotation,
            TSVector scale)
        {
            TSMatrix4x4 matrix;
            TSMatrix4x4.TRS(translation, rotation, scale, out matrix);
            return matrix;
        }
    }
}