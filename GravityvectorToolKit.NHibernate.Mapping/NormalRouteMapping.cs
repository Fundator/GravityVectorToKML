﻿using FluentNHibernate.Mapping;
using GravityVectorToolKit.DataModel;
using NetTopologySuite.Geometries;
using NHibernate.Spatial.Type;

namespace GravityVectorToolKit.CSV.Mapping
{
	public class NormalRouteMapping<T> : ClassMap<NormalRoute> where T : IGeometryUserType
	{
		public NormalRouteMapping()
		{
			ImportType<Geometry>();
			Id(x => x.NormalRouteId).GeneratedBy.Assigned();
			Version(x => x.LastModified);
			Map(x => x.FromLocationId).Index("FromLocationToLocation_Idx");
			Map(x => x.ToLocationId).Index("FromLocationToLocation_Idx");
			Map(x => x.HighError).Index("HighError_Idx");
			Map(x => x.BackboneHarbour).Index("BackboneHarbour_Idx");
			//Map(x => x.AverageSog).CustomSqlType("float[]");
			Map(x => x.VoyageCount).Index("VoyageCount_Idx");
			Map(x => x.AverageSpeed).Index("AverageSpeed_Idx");
			Map(x => x.NormalRouteGeometry).Column("normalroutegeometry").CustomType<T>();
			Map(x => x.NormalRouteMaxGeometry).Column("normalroutemaxgeometry").CustomType<T>();
			Map(x => x.NormalRouteStdGeometry).Column("normalroutestdgeometry").CustomType<T>();
			HasMany(x => x.GravityVectors).KeyColumn(nameof(NormalRoute.NormalRouteId)).Inverse().Cascade.All();
		}
	}
}