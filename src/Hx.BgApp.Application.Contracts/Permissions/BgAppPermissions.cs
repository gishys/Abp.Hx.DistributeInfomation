﻿namespace Hx.BgApp.Permissions;

public static class BgAppPermissions
{
    public const string GroupName = "BgApp";
    public static class Project
    {
        public const string Default = GroupName + ".Project";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }
}