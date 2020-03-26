using Microsoft.Xna.Framework;
using Nez;
using Nez.Persistence;
using Nez.PhysicsShapes;
using Nez.Systems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamJam.Source.Content {
	public static class ContentManagerExt {
		public static List<Vector2[]> LoadPolygons(this NezContentManager content, string name) {
			List<Vector2[]> polys = new List<Vector2[]>();
			using (var stream = TitleContainer.OpenStream(name))
			using (StreamReader sr = new StreamReader(stream)) {
				polys.AddRange(Json.FromJson<Vector2[][]>(sr.ReadToEnd()));
			}
			return polys;
		}
	}
}
