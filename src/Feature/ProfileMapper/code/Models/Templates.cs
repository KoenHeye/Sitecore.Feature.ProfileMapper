using Sitecore.Data;

namespace Sitecore.Feature.ProfileMapper.Models
{
    public struct Templates
    {
        public struct FieldReferenceProfileMap
        {
            public static readonly ID ID = new ID("{8C8AAF62-5D2C-42EB-BB61-3CC75927BAE5}");

            public struct Fields
            {
                public static readonly ID ContextItemField = new ID("{4F7856FB-307B-41B8-8E61-D12DAC5E800B}");

                public static readonly ID ReferencesItem = new ID("{75FA81D9-5936-4C86-96AE-E806ABA3CB96}");
            }
        }

        public struct FieldReferencesProfileMap
        {
            public static readonly ID ID = new ID("{BC139E72-08E9-423D-AF23-238801D68CD7}");

            public struct Fields
            {
                public static readonly ID ContextItemField = new ID("{F743851E-C008-4ED3-A1BA-411A92C0F619}");

                public static readonly ID ReferencesItems = new ID("{87F8567C-26D7-4D9F-9F11-79DA685A743C}");

                public static readonly ID MatchAll = new ID("{02999048-80A9-46DD-94D5-C0BA20273923}");
            }
        }

        public struct FieldRangeProfileMap
        {
            public static readonly ID ID = new ID("{7ECF465D-274F-4C31-A86F-738D4DE0E97E}");

            public struct Fields
            {
                public static readonly ID ContextItemField = new ID("{BEF550CB-B0F0-404D-974D-321003547786}");

                public static readonly ID MinValue = new ID("{805313FB-B5AB-4E1A-A25A-CCA592D83107}");

                public static readonly ID MaxValue = new ID("{E5B4B80F-3EFE-457C-8108-D18E8E0C220C}");
            }
        }

        public struct FieldDateRangeProfileMap
        {
            public static readonly ID ID = new ID("{3586D611-FBDF-47BC-8A2D-E2188164AF28}");

            public struct Fields
            {
                public static readonly ID ContextItemField = new ID("{AFE4F915-C998-4E30-A4D4-819EC52BBB9B}");

                public static readonly ID DateFrom = new ID("{C9DC4349-4E29-4B19-9617-EF29DF2A099A}");

                public static readonly ID DateTo = new ID("{8762272E-5F57-4F15-B878-8F9450693815}");

                public static readonly ID RepeatEveryYear = new ID("{92EC0455-276F-48FA-9CA2-30F8695B912A}");
            }
        }

        public struct FieldContainsProfileMap
        {
            public static readonly ID ID = new ID("{3074C271-0FF0-47C5-A945-2E7641C846B7}");

            public struct Fields
            {
                public static readonly ID ContextItemField = new ID("{A2DCA904-B600-4CC0-8A6D-22686E2ABDC5}");

                public static readonly ID StringsToMatch = new ID("{6A954D93-44B7-49FD-ADFE-1A04CD0A7F27}");
            }
        }      

        public struct IsDescendantOfProfileMap
        {
            public static readonly ID ID = new ID("{92EEB249-80A1-467E-805C-BE7C15093E75}");

            public struct Fields
            {
                public static readonly ID Ancestor = new ID("{9DB5C1C7-707C-400D-8298-7515A17EE084}");
            }
        }

        public struct RulesProfileMap
        {
            public static readonly ID ID = new ID("{F2B85A12-8E22-447F-A003-0F9FD66A8BCB}");

            public struct Fields
            {
                public static readonly ID Rules = new ID("{A7BF5C24-C62D-4B4A-8EAA-F60ED92AF76D}");
            }
        }       

        public struct ProfileMapSet
        {
            public static readonly ID ID = new ID("{62C3232E-C7AA-4CE6-8A59-BA118E883516}");

            public struct Fields
            {
                public static readonly ID MatchSingle = new ID("{6B143C50-598C-4F30-B265-11B1111C7585}");

                public static readonly ID Enabled = new ID("{120BA0BF-2EFB-4336-9991-16E1B23B33DB}");
            }
        }

        public struct ProfileMap
        {
            public static readonly ID ID = new ID("{BD16181E-5DFE-4389-AD05-1E766464BF62}");

            public struct Fields
            {
                public static readonly ID ProfileMapAssembly = new ID("{5A174271-08F5-455F-8574-0BADE34FA0EE}");

                public static readonly ID ProfileMapClass = new ID("{688E394B-CD85-42A0-8658-869C6BD6F629}");
            }
        }

        public struct ProfileMapFolder
        {
            public static ID ID = new ID("{A5A9E239-BB8B-4979-B781-31FFAF3D4CD7}");
        }

        public struct ProfileMapSite
        {
            public static ID ID = new ID("{658E8001-E0EC-400A-9FED-1422B87BEFC4}");

            public struct Fields
            {
                public static readonly ID SiteName = new ID("{C58E3849-7C83-4D3C-A04F-D36828924D97}");
            }
        }        
    }
}