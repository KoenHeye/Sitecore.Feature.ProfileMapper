using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Shell.Framework.Pipelines;
using Sitecore.Web.UI.Sheer;
using Sitecore.StringExtensions;

namespace Sitecore.Feature.ProfileMapper.Shell.Framework.Pipelines
{
    public class ScaffoldProfileKey : ItemOperation
    {
        public virtual void GetProfileKeyName(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            if (!args.IsPostBack)
            {
                var db = Database.GetDatabase(args.Parameters["database"]);
                var language = Language.Parse(args.Parameters["language"]);
                var parent = db.GetItem(args.Parameters["id"], language);

                Context.ClientPage.ClientResponse.Input("Enter a new name for the profile key:", "", Settings.ItemNameValidation, "'$Input' is not a valid name.", Settings.MaxItemNameLength, "Profile Key Name");

                args.WaitForPostBack();

                return;
            }
            if (args.Result == null || args.Result.Length == 0 || args.Result == "null" || args.Result == "undefined")
            {
                args.AbortPipeline();

                return;
            }

            if (args.Result.Trim().Length == 0)
            {
                Context.ClientPage.ClientResponse.Alert("The name cannot be blank.");
                Context.ClientPage.ClientResponse.Input("Enter a new name for the profile key:", string.Empty, Settings.ItemNameValidation, "'$Input' is not a valid name.", Settings.MaxItemNameLength, "Profile Key Name");

                args.WaitForPostBack();

                return;
            }

            Context.ClientPage.ServerProperties["Name"] = args.Result;

            args.IsPostBack = false;

            return;
        }

        public virtual void ExecuteScaffolding(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            var db = Database.GetDatabase(args.Parameters["database"]);
            var language = Language.Parse(args.Parameters["language"]);
            var profile = db.GetItem(args.Parameters["id"], language);
            var name = StringUtil.GetString(Context.ClientPage.ServerProperties["Name"]);

            var key = profile.Children[name] ?? profile.Add(name, new TemplateID(ID.Parse("{44AB5107-3C73-42F0-A427-BEC549F944B9}")));

            key.Editing.BeginEdit();

            key["MinValue"] = "0";
            key["MaxValue"] = "10";

            key.Editing.EndEdit();

            ExecuteProfileCardScaffolding(profile, key);
            ExecutePatternCardScaffolding(profile, key);
        }

        public virtual void ExecuteProfileCardScaffolding(Item profile, Item key)
        {
            var folder = profile.Children["Profile Cards"];

            var card = folder.Children[key.Name] ?? folder.Add(key.Name, new TemplateID(ID.Parse("{0FC09EA4-8D87-4B0E-A5C9-8076AE863D9C}")));

            card.Editing.BeginEdit();

            card["Profile Card Value"] = "<tracking><profile id=\"{0}\" name=\"{1}\"><key name=\"{2}\" value=\"10\" /></profile></tracking>".FormatWith(profile.ID.Guid.ToString("D").ToLowerInvariant(), profile.Name,key.Name);

            card.Editing.EndEdit();
        }

        public virtual void ExecutePatternCardScaffolding(Item profile, Item key)
        {
            var folder = profile.Children["Pattern Cards"];

            var card = folder.Children[key.Name] ?? folder.Add(key.Name, new TemplateID(ID.Parse("{4A6A7E36-2481-438F-A9BA-0453ECC638FA}")));

            card.Editing.BeginEdit();

            card["Pattern"] = "<tracking><profile id=\"{0}\" name=\"{1}\"><key name=\"{2}\" value=\"10\" /></profile></tracking>".FormatWith(profile.ID.Guid.ToString("D").ToLowerInvariant(), profile.Name, key.Name);

            card.Editing.EndEdit();
        }
    }
}