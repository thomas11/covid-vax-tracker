using Pulumi;
using AzureNative = Pulumi.AzureNative;
using System.Collections.Generic;

class MyStack : Stack
{
    public MyStack()
    {
        var cvtRG = new AzureNative.Resources.ResourceGroup("cvtRG", new AzureNative.Resources.ResourceGroupArgs
        {
            Location = "westus2",
            ResourceGroupName = "covidvaxtracker2",
        }, new CustomResourceOptions
        {
            Protect = true,
        });
        var cvtStorageAccount = new AzureNative.Storage.StorageAccount("cvtStorageAccount", new AzureNative.Storage.StorageAccountArgs
        {
            AccountName = "covidvaxtracker2",
            EnableHttpsTrafficOnly = true,
            Encryption = new AzureNative.Storage.Inputs.EncryptionArgs
            {
                KeySource = "Microsoft.Storage",
                Services = new AzureNative.Storage.Inputs.EncryptionServicesArgs
                {
                    Blob = new AzureNative.Storage.Inputs.EncryptionServiceArgs
                    {
                        Enabled = true,
                        KeyType = "Account",
                    },
                    File = new AzureNative.Storage.Inputs.EncryptionServiceArgs
                    {
                        Enabled = true,
                        KeyType = "Account",
                    },
                },
            },
            Kind = "Storage",
            Location = "westus2",
            NetworkRuleSet = new AzureNative.Storage.Inputs.NetworkRuleSetArgs
            {
                Bypass = "AzureServices",
                DefaultAction = "Allow",
            },
            ResourceGroupName = "covidvaxtracker2",
            Sku = new AzureNative.Storage.Inputs.SkuArgs
            {
                Name = "Standard_LRS",
            },
        }, new CustomResourceOptions
        {
            Protect = true,
        });
        var cvtKeyVault = new AzureNative.KeyVault.Vault("cvtKeyVault", new AzureNative.KeyVault.VaultArgs
        {
            Location = "westus2",
            Properties = new AzureNative.KeyVault.Inputs.VaultPropertiesArgs
            {
                AccessPolicies =
                {
                    new AzureNative.KeyVault.Inputs.AccessPolicyEntryArgs
                    {
                        ObjectId = "e6b12deb-d6f3-4c9b-b364-a99b376f55de",
                        Permissions = new AzureNative.KeyVault.Inputs.PermissionsArgs
                        {
                            Keys =
                            {
                                "Get",
                            },
                            Secrets =
                            {
                                "Get",
                            },
                        },
                        TenantId = "51920507-9d80-44df-89ad-f7a897aad167",
                    },
                    new AzureNative.KeyVault.Inputs.AccessPolicyEntryArgs
                    {
                        ObjectId = "ce7f158b-1ce1-413f-a819-468b795e0522",
                        Permissions = new AzureNative.KeyVault.Inputs.PermissionsArgs
                        {
                            Certificates =
                            {
                                "Get",
                                "List",
                                "Update",
                                "Create",
                                "Import",
                                "Delete",
                                "Recover",
                                "Backup",
                                "Restore",
                                "ManageContacts",
                                "ManageIssuers",
                                "GetIssuers",
                                "ListIssuers",
                                "SetIssuers",
                                "DeleteIssuers",
                            },
                            Keys =
                            {
                                "Get",
                                "List",
                                "Update",
                                "Create",
                                "Import",
                                "Delete",
                                "Recover",
                                "Backup",
                                "Restore",
                            },
                            Secrets =
                            {
                                "Get",
                                "List",
                                "Set",
                                "Delete",
                                "Recover",
                                "Backup",
                                "Restore",
                            },
                        },
                        TenantId = "51920507-9d80-44df-89ad-f7a897aad167",
                    },
                },
                EnableRbacAuthorization = false,
                EnableSoftDelete = true,
                EnabledForDeployment = false,
                EnabledForDiskEncryption = false,
                EnabledForTemplateDeployment = false,
                ProvisioningState = "Succeeded",
                Sku = new AzureNative.KeyVault.Inputs.SkuArgs
                {
                    Family = "A",
                    Name = "Standard",
                },
                SoftDeleteRetentionInDays = 90,
                TenantId = "51920507-9d80-44df-89ad-f7a897aad167",
                VaultUri = "https://covidvaxtracker-vault.vault.azure.net/",
            },
            ResourceGroupName = "covidvaxtracker2",
            VaultName = "covidvaxtracker-vault",
        }, new CustomResourceOptions
        {
            Protect = true,
        });
        var cvtAppPlan = new AzureNative.Web.AppServicePlan("cvtAppPlan", new AzureNative.Web.AppServicePlanArgs
        {
            HyperV = false,
            IsSpot = false,
            IsXenon = false,
            Kind = "functionapp",
            Location = "West US 2",
            MaximumElasticWorkerCount = 1,
            Name = "WestUS2Plan",
            PerSiteScaling = false,
            Reserved = false,
            ResourceGroupName = "covidvaxtracker2",
            Sku = new AzureNative.Web.Inputs.SkuDescriptionArgs
            {
                Capacity = 0,
                Family = "Y",
                Name = "Y1",
                Size = "Y1",
                Tier = "Dynamic",
            },
            TargetWorkerCount = 0,
            TargetWorkerSizeId = 0,
        }, new CustomResourceOptions
        {
            Protect = true,
        });
        var cvtFunctionApp = new AzureNative.Web.WebApp("cvtFunctionApp", new AzureNative.Web.WebAppArgs
        {
            ClientAffinityEnabled = false,
            ClientCertEnabled = false,
            ClientCertMode = "Required",
            ContainerSize = 1536,
            CustomDomainVerificationId = "D19BDF63406458B49AECDDBEB9110CF4634CA90BA01AEF3D9F00BB79AB522134",
            DailyMemoryTimeQuota = 0,
            Enabled = true,
            HostNameSslStates =
            {
                new AzureNative.Web.Inputs.HostNameSslStateArgs
                {
                    HostType = "Standard",
                    Name = "covidvaxtracker.azurewebsites.net",
                    SslState = "Disabled",
                },
                new AzureNative.Web.Inputs.HostNameSslStateArgs
                {
                    HostType = "Repository",
                    Name = "covidvaxtracker.scm.azurewebsites.net",
                    SslState = "Disabled",
                },
            },
            HostNamesDisabled = false,
            HttpsOnly = false,
            HyperV = false,
            Identity = new AzureNative.Web.Inputs.ManagedServiceIdentityArgs
            {
                Type = "SystemAssigned",
            },
            IsXenon = false,
            KeyVaultReferenceIdentity = "SystemAssigned",
            Kind = "functionapp",
            Location = "West US 2",
            Name = "covidvaxtracker",
            RedundancyMode = "None",
            Reserved = false,
            ResourceGroupName = "covidvaxtracker2",
            ScmSiteAlsoStopped = false,
            ServerFarmId = "/subscriptions/ecf88c95-e79e-4ecb-81af-c2c6b6063fea/resourceGroups/covidvaxtracker2/providers/Microsoft.Web/serverfarms/WestUS2Plan",
            SiteConfig = new AzureNative.Web.Inputs.SiteConfigArgs
            {
                AcrUseManagedIdentityCreds = false,
                AlwaysOn = false,
                FunctionAppScaleLimit = 200,
                Http20Enabled = false,
                LinuxFxVersion = "",
                MinimumElasticInstanceCount = 0,
                NumberOfWorkers = 1,
            },
            StorageAccountRequired = false,
        }, new CustomResourceOptions
        {
            Protect = true,
        });
        var cvtTimerFunction = new AzureNative.Web.WebAppFunction("cvtTimerFunction", new AzureNative.Web.WebAppFunctionArgs
        {
            Config = new Dictionary<string, string>
            {
                { "bindings",
                {

                    {
                        { "name", "myTimer" },
                        { "runOnStartup", false },
                        { "schedule", "0 0 1 * * *" },
                        { "type", "timerTrigger" },
                        { "useMonitor", true },
                    },
                } },
                { "configurationSource", "attributes" },
                { "disabled", false },
                { "entryPoint", "CdcVaxTracker.Function.cdc_vax_function.runTimer" },
                { "generatedBy", "Microsoft.NET.Sdk.Functions-3.0.11" },
                { "scriptFile", "../bin/cdc_vax_function.dll" },
            },
            ConfigHref = "https://covidvaxtracker.azurewebsites.net/admin/vfs/site/wwwroot/TweetCdcDataTimer/function.json",
            FunctionName = "TweetCdcDataTimer",
            Href = "https://covidvaxtracker.azurewebsites.net/admin/functions/TweetCdcDataTimer",
            IsDisabled = false,
            Language = "DotNetAssembly",
            Name = "covidvaxtracker",
            ResourceGroupName = "covidvaxtracker2",
            ScriptHref = "https://covidvaxtracker.azurewebsites.net/admin/vfs/site/wwwroot/bin/cdc_vax_function.dll",
            ScriptRootPathHref = "https://covidvaxtracker.azurewebsites.net/admin/vfs/site/wwwroot/TweetCdcDataTimer/",
            TestData = "",
            TestDataHref = "https://covidvaxtracker.azurewebsites.net/admin/vfs/data/Functions/sampledata/TweetCdcDataTimer.dat",
        }, new CustomResourceOptions
        {
            Protect = true,
        });
    }
}