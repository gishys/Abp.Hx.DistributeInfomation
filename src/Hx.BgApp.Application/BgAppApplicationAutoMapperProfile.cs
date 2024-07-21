using AutoMapper;
using Hx.BgApp.Layout;
using Hx.BgApp.PublishInformation;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace Hx.BgApp;

public class BgAppApplicationAutoMapperProfile : Profile
{
    public BgAppApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Project, ProjectDto>(MemberList.Destination);
        CreateMap<Menu, MenuDto>(MemberList.Destination);
        CreateMap<Page, PageDto>(MemberList.Destination);

        CreateMap<PublishFeadbackInfo, PublishFeadbackInfoDto>(MemberList.Destination);
        CreateMap<ContentInfo, ContentInfoDto>(MemberList.Destination);
    }
    public string ToGeoJsonFeature(Geometry? record)
    {
        var geoJsonWriter = new GeoJsonWriter();
        return geoJsonWriter.Write(record);
    }
}