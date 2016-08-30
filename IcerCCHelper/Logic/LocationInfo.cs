namespace IcerDesign.CCHelper
{
    public enum DriverVolume
    {
        None,
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        J,
        I,
        K,
        L,
        M,
        N,
        O,
        P,
        Q,
        R,
        S,
        T,
        U,
        V,
        W,
        X,
        Y,
        Z,
    }

    public struct LocationInfo
    {
        public DriverVolume Volume { get; }
        public string VOBName { get; }
        public string BasePath => $@"{this.Volume}:\{this.VOBName}";

        public LocationInfo(DriverVolume volume, string vobName)
        {
            this.VOBName = vobName;
            this.Volume = volume;
        }
    }
}