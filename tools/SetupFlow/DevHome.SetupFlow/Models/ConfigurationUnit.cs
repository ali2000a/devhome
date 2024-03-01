﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Linq;
using OutOfProcConfigurationUnit = Microsoft.Management.Configuration.ConfigurationUnit;

namespace DevHome.SetupFlow.Models;

public class ConfigurationUnit
{
    private const string DescriptionSettingsKey = "description";
    private const string ModuleMetadataKey = "module";

    public ConfigurationUnit(OutOfProcConfigurationUnit unit)
    {
        Type = unit.Type;
        Id = unit.Identifier;
        Intent = unit.Intent.ToString();
        Dependencies = [.. unit.Dependencies];

        // Get description from settings
        unit.Settings.TryGetValue(DescriptionSettingsKey, out var descriptionObj);
        Description = descriptionObj?.ToString() ?? string.Empty;

        // Get module name from metadata
        unit.Metadata.TryGetValue(ModuleMetadataKey, out var moduleObj);
        ModuleName = moduleObj?.ToString() ?? string.Empty;

        Settings = unit.Settings.Select(s => new KeyValuePair<string, string>(s.Key, s.Value.ToString())).ToList();
        Metadata = unit.Metadata.Select(m => new KeyValuePair<string, string>(m.Key, m.Value.ToString())).ToList();
    }

    public string Type { get; }

    public string Id { get; }

    public string Description { get; }

    public string ModuleName { get; }

    public string Intent { get; }

    public IList<string> Dependencies { get; }

    public IList<KeyValuePair<string, string>> Settings { get; }

    public IList<KeyValuePair<string, string>> Metadata { get; }
}
