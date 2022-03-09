// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("6Zdt7Q3fXbWtjp+wlYoiY72HkijCP8jI7LTLb9RrmA7uj55bZbzmt4s5upmLtr2ykT3zPUy2urq6vru4xzH+b1z07yu4b5F980pCnMp9POt3ozXE7tyJUd5RIiKr7wl31MtdHjsNp0za4ZfnqGsy+QSVFaEAqHL2Obq0u4s5urG5Obq6uy7HXOtWo5Td9+byD7jV83ICGppGm0ihEBdNew0DOOhEi7TWLRQ5pP/S/+MZR3GmqQ+fR2KGQ2bZ4Phe6+Nbk8eoMF7rL3AOWz+HVSFR/4zM8tKjoDa4WG7WxtDsOnm8tLNO/Zbd13pEJ4+NsKhWBm62St6PY3o4szb6ShLZbTIEFfi9PT7/nkls6FkTDTxeMRW3mfybkEoeyYXyTrm4uru6");
        private static int[] order = new int[] { 13,2,13,10,9,10,13,13,12,9,11,13,13,13,14 };
        private static int key = 187;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
