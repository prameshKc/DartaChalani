using System.Linq;
using AutoMapper;
using DomainModel;
using DomainModel.FieldRekhankanDarta;
using DomainModel.HalsabikChalani;
using DomainModel.HalsabikDarta;
using DomainModel.Resources;
using DomainModel.Resources.FieldRekhankanDarta;
using DomainModel.Resources.HalsabikChalaniResource;
using DomainModel.Resources.HalsabikDarta;
using DomainModel.Resources.Setting;
using DomainModel.Setting;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Resources;

namespace services.Mapper {
    public class MapperProfile : Profile {

        public MapperProfile () {

            #region Resource To Api Mapping
            CreateMap<UserResource, ApplicationUser> ();

            CreateMap<SiteSettingResource, SiteSetting> ()
                .ForMember (p => p.Names, m => m.MapFrom (s => s.Names));

            CreateMap<ChalanResource, Chalan> ();

            CreateMap<DartaResource, Dartas> ();

            CreateMap<SubjectResource, Subject> ();

            CreateMap<ChalanFileResource, ChalanFiles> ();
            CreateMap<PurjiDartaFileResource, PurjiDartaFiles> ();
            CreateMap<RekhankanChalanFileResource, RekhankanChalanFiles> ();
            CreateMap<RekhankanDartaFileResource, FieldRekhankanDartaFile> ();
            CreateMap<HalsabikChalaniFileResource, HalsabikChalaniFile> ();
            CreateMap<HalsabikDartaFileResource, HalsabikDartaFile> ();

            CreateMap<ChitthiPurjiResource, ChitthiPurji> ()
                .ForMember (p => p.patras, opt => opt.MapFrom (s => s.patras))
                .AfterMap ((r, d) => {
                    var addedPatras = r.patras.Where (p => !d.patras.Any (c => c.file.fileUrl == p.file.fileUrl)).Select (p =>
                        new ChalanPatras () {
                            fileId = p.file.Id
                        }).ToList ();

                    if (addedPatras.Count > 0) {
                        foreach (var c in addedPatras) {
                            d.patras.Add (c);
                        }
                    }
                });

            CreateMap<HalsabikChalaniResource, HalsabikChalani> ()
                .ForMember (p => p.patras, opt => opt.MapFrom (s => s.patras))
                .AfterMap ((r, d) => {
                    var addedPatras = r.patras.Where (p => !d.patras.Any (c => c.file.fileUrl == p.file.fileUrl)).Select (p =>
                        new HalsabikChalaniPatras () {
                            fileId = p.file.Id
                        }).ToList ();

                    if (addedPatras.Count > 0) {
                        foreach (var c in addedPatras) {
                            d.patras.Add (c);
                        }
                    }
                });

            CreateMap<HalsabikDartaResource, HalsabikDarta> ()
                .ForMember (p => p.patras, opt => opt.MapFrom (s => s.patras))
                .AfterMap ((r, d) => {
                    var addedPatras = r.patras.Where (p => !d.patras.Any (c => c.file.fileUrl == p.file.fileUrl)).Select (p =>
                        new HalsabikDartaPatras () {
                            fileId = p.file.Id
                        }).ToList ();

                    if (addedPatras.Count > 0) {
                        foreach (var c in addedPatras) {
                            d.patras.Add (c);
                        }
                    }
                });

            CreateMap<ChitthiPurjiDartaResource, ChitthiPurjiDarta> ()
                .ForMember (p => p.patras, opt => opt.MapFrom (s => s.patras))
                .AfterMap ((r, d) => {
                    var addedPatras = r.patras.Where (p => !d.patras.Any (c => c.file.fileUrl == p.file.fileUrl))
                        .Select (p =>
                            new ChitthiDartaPatras () {
                                fileId = p.file.Id
                            }).ToList ();

                    if (addedPatras.Count > 0) {
                        foreach (var c in addedPatras) {
                            d.patras.Add (c);
                        }
                    }
                });

            CreateMap<FieldRekhankanChalaniResource, FieldRekhankanChalani> ()
                .ForMember (p => p.patras, opt => opt.MapFrom (s => s.patras))
                .AfterMap ((r, d) => {
                    var addedPatras = r.patras.Where (p => !d.patras.Any (c => c.file.fileUrl == p.file.fileUrl))
                        .Select (p =>
                            new RekhankanChalanPatras () {
                                fileId = p.file.Id
                            }).ToList ();

                    if (addedPatras.Count > 0) {
                        foreach (var c in addedPatras) {
                            d.patras.Add (c);
                        }
                    }
                });
            CreateMap<FieldRekhankanDartaResource, FieldRekhankanDarta> ()
                .ForMember (p => p.patras, opt => opt.MapFrom (s => s.patras))
                .AfterMap ((r, d) => {
                    var addedPatras = r.patras.Where (p => !d.patras.Any (c => c.file.fileUrl == p.file.fileUrl))
                        .Select (p =>
                            new RekhankanDartaPatras () {
                                fileId = p.file.Id
                            }).ToList ();

                    if (addedPatras.Count > 0) {
                        foreach (var c in addedPatras) {
                            d.patras.Add (c);
                        }
                    }
                });

            CreateMap<RekhankanChalanPatraResource, RekhankanChalanPatras> ()
                .ForMember (p => p.file, m => m.MapFrom (s => s.file));

            CreateMap<PurjiDartaPatraResource, ChitthiDartaPatras> ()
                .ForMember (p => p.file, m => m.MapFrom (s => s.file));
                 CreateMap<ChalanPatrasResource, ChalanPatras> ()
                .ForMember (p => p.file, m => m.MapFrom (s => s.file));


            CreateMap<RekhankanDartaPatrasResource, RekhankanDartaPatras> ()
                .ForMember (p => p.file, m => m.MapFrom (s => s.file));

            CreateMap<HalsabikChalaniPatraResource, HalsabikChalaniPatras> ()
                .ForMember (p => p.file, m => m.MapFrom (s => s.file));

            CreateMap<HalsabikDartaPatraResource, HalsabikDartaPatras> ()
                .ForMember (p => p.file, m => m.MapFrom (s => s.file));
            CreateMap<OfficeNameResource, OfficeName> ();
            #endregion

            #region  DomainModel To Resource Mapping

            CreateMap<ApplicationUser, UserResource> ()
                .ForMember (p => p.fullName, m => m.MapFrom (s => s.firstName + " " + s.lastName));

            CreateMap<SiteSetting, SiteSettingResource> ();
            CreateMap<OfficeName, OfficeNameResource> ();
            CreateMap<ApplicationRole, RoleResource> ();

            CreateMap<ChitthiPurji, ChitthiPurjiResource> ()
                .ForMember (p => p.subject, m => m.MapFrom (s => s.subject))
                .ForMember (p => p.chalan, m => m.MapFrom (s => s.chalan))
                .ForMember (p => p.patras, m => m.MapFrom (s => s.patras));
            //      .ForMember (p => p.patraUrl, m => m.MapFrom (s => s.files.Select (f => f.fileUrl)));
            CreateMap<HalsabikChalani, HalsabikChalaniResource> ()
                .ForMember (p => p.subject, m => m.MapFrom (s => s.subject))
                .ForMember (p => p.chalan, m => m.MapFrom (s => s.chalan))
                .ForMember (p => p.patras, m => m.MapFrom (s => s.patras));
            CreateMap<HalsabikDarta, HalsabikDartaResource> ()
                .ForMember (p => p.patras, m => m.MapFrom (s => s.patras));

            CreateMap<FieldRekhankanChalani, FieldRekhankanChalaniResource> ()
                .ForMember (p => p.subject, m => m.MapFrom (s => s.subject))
                .ForMember (p => p.chalan, m => m.MapFrom (s => s.chalan))
                .ForMember (p => p.patras, m => m.MapFrom (s => s.patras));
            //      .ForMember (p => p.patraUrl, m => m.MapFrom (s => s.files.Select (f => f.fileUrl)));

            CreateMap<FieldRekhankanDarta, FieldRekhankanDartaResource> ()
                .ForMember (p => p.patras, m => m.MapFrom (s => s.patras));
            //      .ForMember (p => p.patraUrl, m => m.MapFrom (s => s.files.Select (f => f.fileUrl)));

            CreateMap<ChitthiPurjiDarta, ChitthiPurjiDartaResource> ()
                .ForMember (p => p.subject, m => m.MapFrom (s => s.subject))
                .ForMember (p => p.DartaType, m => m.MapFrom (s => s.DartaType))
                .ForMember (p => p.patras, m => m.MapFrom (s => s.patras));

            CreateMap<Subject, SubjectResource> ();
            CreateMap<Chalan, ChalanResource> ();
            CreateMap<Dartas, DartaResource> ();

            CreateMap<ChalanPatras, ChalanPatrasResource> ()
                .ForMember (p => p.file, m => m.MapFrom (s => s.file));
            CreateMap<RekhankanChalanPatras, RekhankanChalanPatraResource> ()
                .ForMember (p => p.file, m => m.MapFrom (s => s.file));

            CreateMap<RekhankanDartaPatras, RekhankanDartaPatrasResource> ()
                .ForMember (p => p.file, m => m.MapFrom (s => s.file));

            CreateMap<ChitthiDartaPatras, PurjiDartaPatraResource> ()
                .ForMember (p => p.file, m => m.MapFrom (s => s.file));
            CreateMap<HalsabikChalaniPatras, HalsabikChalaniPatraResource> ()
                .ForMember (p => p.file, m => m.MapFrom (s => s.file));
            CreateMap<HalsabikDartaPatras, HalsabikDartaPatraResource> ()
                .ForMember (p => p.file, m => m.MapFrom (s => s.file));
            CreateMap<ChalanFiles, ChalanFileResource> ();
            CreateMap<RekhankanChalanFiles, RekhankanChalanFileResource> ();
            CreateMap<PurjiDartaFiles, PurjiDartaFileResource> ();
            CreateMap<FieldRekhankanDartaFile, RekhankanDartaFileResource> ();
            CreateMap<HalsabikChalaniFile, HalsabikChalaniFileResource> ();
            CreateMap<HalsabikDartaFile, HalsabikDartaFileResource> ();
            #endregion
        }
    }
}