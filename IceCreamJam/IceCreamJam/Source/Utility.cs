namespace IceCreamJam.Source {
    static class Utility {
        public static int Mod(int x, int m) {
            return (x % m + m) % m;
        }
    }
}
