using Terraria.ModLoader;

namespace YHTMod {
	public class YHTMod : Mod {
		private static YHTMod mod;
		public YHTMod() {
			mod = this;
		}

		public static YHTMod GetInstance() {
			return mod;
		}
	}
}