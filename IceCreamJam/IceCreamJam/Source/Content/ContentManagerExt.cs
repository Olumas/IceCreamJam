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
		public static List<Polygon> LoadPolygons(this NezContentManager content, string name) {
			List<Polygon> polys = new List<Polygon>();
			using (var stream = TitleContainer.OpenStream(name))
			using (StreamReader sr = new StreamReader(stream)) {
				Vector2[][] points = Json.FromJson<Vector2[][]>(sr.ReadToEnd());
				polys.AddRange(points.Select(x => new Polygon(x.ToArray())));
			}
			return polys;
		}
	}
}
