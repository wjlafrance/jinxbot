// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project. 
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc. 
//
// To add a suppression to this file, right-click the message in the 
// Error List, point to "Suppress Message(s)", and click 
// "In Project Suppression File". 
// You do not need to add suppressions to this file manually. 

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "BNSharp.ChannelListEventArgs.#Channels")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Scope = "type", Target = "BNSharp.Priority")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "BNSharp.MBNCSUtil.CdKey.#Value2")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Scope = "member", Target = "BNSharp.MBNCSUtil.CheckRevision.#DoCheckRevision(System.String,System.String[],System.Int32)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "4#", Scope = "member", Target = "BNSharp.MBNCSUtil.CheckRevision.#DoLockdownCheckRevision(System.Byte[],System.String[],System.String,System.String,System.Int32&,System.Int32&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "5#", Scope = "member", Target = "BNSharp.MBNCSUtil.CheckRevision.#DoLockdownCheckRevision(System.Byte[],System.String[],System.String,System.String,System.Int32&,System.Int32&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Scope = "member", Target = "BNSharp.MBNCSUtil.CheckRevision.#GetExeInfo(System.String,System.String&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "BNSharp.Net")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "BNSharp.Plugins")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "BNSharp.MBNCSUtil.DataBuffer.#UnderlyingBuffer")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "BNSharp.MBNCSUtil.DataReader.#Data")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "expectedItems*2", Scope = "member", Target = "BNSharp.MBNCSUtil.DataReader.#ReadInt16Array(System.Int32)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "expectedItems*4", Scope = "member", Target = "BNSharp.MBNCSUtil.DataReader.#ReadInt32Array(System.Int32)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "expectedItems*8", Scope = "member", Target = "BNSharp.MBNCSUtil.DataReader.#ReadInt64Array(System.Int32)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "expectedItems*2", Scope = "member", Target = "BNSharp.MBNCSUtil.DataReader.#ReadUInt16Array(System.Int32)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "expectedItems*4", Scope = "member", Target = "BNSharp.MBNCSUtil.DataReader.#ReadUInt32Array(System.Int32)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "expectedItems*8", Scope = "member", Target = "BNSharp.MBNCSUtil.DataReader.#ReadUInt64Array(System.Int32)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "BNSharp.MBNCSUtil.DataReader.#UnderlyingBuffer")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Scope = "member", Target = "BNSharp.MBNCSUtil.XSha1.#SafeHash(System.Byte[])")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "BNSharp.MBNCSUtil.Data.MpqArchive.#GetListFile()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable", Scope = "type", Target = "BNSharp.MBNCSUtil.Data.MpqServices")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "BNSharp.MBNCSUtil.Data.MpqServices+SingletonHost.#.cctor()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "BNSharp.MBNCSUtil.Net.BnFtpVersion1Request.#.ctor(System.String,System.String,System.Nullable`1<System.DateTime>)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "BNSharp.MBNCSUtil.Net.BnFtpVersion2Request.#.ctor(System.String,System.String,System.DateTime,System.String)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "1", Scope = "member", Target = "BNSharp.MBNCSUtil.Data.NativeMethods.#GetProcAddress(System.IntPtr,System.String)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "0", Scope = "member", Target = "BNSharp.MBNCSUtil.Data.NativeMethods.#LoadLibrary(System.String)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "BNSharp.MBNCSUtil.Util.PeFileReader+ImageResourceDirectoryEntry.#Id")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "BNSharp.MBNCSUtil.Util.PeFileReader+ImageResourceDirectoryEntry.#Name")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "BNSharp.MBNCSUtil.Util.PeFileReader+ImageResourceDirectoryEntry.#OffsetToData")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "BNSharp.MBNCSUtil.Util.PeFileReader+ImageSectionHeader.#Name")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "BNSharp.MBNCSUtil.Util.PeFileReader+ImageSectionHeader.#NumberOfLinenumbers")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "BNSharp.MBNCSUtil.Util.PeFileReader+ImageSectionHeader.#NumberOfRelocations")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "BNSharp.MBNCSUtil.Util.PeFileReader+ImageSectionHeader.#PhysicalAddress")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "BNSharp.MBNCSUtil.Util.PeFileReader+ImageSectionHeader.#PointerToLinenumbers")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "BNSharp.MBNCSUtil.Util.PeFileReader+ImageSectionHeader.#PointerToRelocations")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "BNSharp.MBNCSUtil.Util.PeFileReader+NtHeaders.#Characteristics")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "BNSharp.MBNCSUtil.Util.PeFileReader+NtHeaders.#Machine")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "BNSharp.MBNCSUtil.Util.PeFileReader+NtHeaders.#NumberOfSymbols")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "BNSharp.MBNCSUtil.Util.PeFileReader+NtHeaders.#PointerToSymbolTable")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "BNSharp.MBNCSUtil.Util.PeFileReader+NtHeaders.#TimeDateStamp")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "BNSharp.Net.BattleNetClient.#HandleWarcraftRequestLadderMap(BNSharp.Net.BattleNetClient+ParseData,BNSharp.MBNCSUtil.DataReader)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "BNSharp.Net.BattleNetClient.#m_loginType")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "BNSharp.Net.BattleNetClient.#m_mpqFiletime")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "BNSharp.Net.BattleNetClient.#m_udpVal")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Scope = "member", Target = "BNSharp.Net.BattleNetClient.#Parse()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "BNSharp.Net.BattleNetClient.#ReportException(System.Exception,System.Collections.Generic.KeyValuePair`2<System.String,System.Object>[])")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "ex", Scope = "member", Target = "BNSharp.Net.BattleNetClient.#ReportException(System.Exception,System.Collections.Generic.KeyValuePair`2<System.String,System.Object>[])")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "notes", Scope = "member", Target = "BNSharp.Net.BattleNetClient.#ReportException(System.Exception,System.Collections.Generic.KeyValuePair`2<System.String,System.Object>[])")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "BNSharp.MBNCSUtil.Util.PeFileReader+ImageResourceDirectoryEntry.#NameOffsetVector")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "BNSharp.MBNCSUtil.Util.PeFileReader+ImageResourceDirectoryEntry.#DirectoryOffsetVector")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member", Target = "BNSharp.BattleNet.Product.#ChatClient")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member", Target = "BNSharp.BattleNet.Product.#Diablo2Expansion")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member", Target = "BNSharp.BattleNet.Product.#Diablo2Retail")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member", Target = "BNSharp.BattleNet.Product.#Diablo2Shareware")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member", Target = "BNSharp.BattleNet.Product.#JapanStarcraft")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member", Target = "BNSharp.BattleNet.Product.#StarcraftRetail")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member", Target = "BNSharp.BattleNet.Product.#StarcraftShareware")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member", Target = "BNSharp.BattleNet.Product.#Warcraft2BNE")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member", Target = "BNSharp.BattleNet.Product.#Warcraft3Expansion")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member", Target = "BNSharp.BattleNet.Product.#Warcraft3Retail")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member", Target = "BNSharp.BattleNet.Product.#StarcraftBroodWar")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "BNSharp.MBNCSUtil.Data.BniFileParser.#AllIcons")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "BNSharp.MBNCSUtil.Data.BniIcon.#SoftwareProductCodes")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "BNSharp.BattleNet.ServerNewsEventArgs.#Entries")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "BNSharp.UserEventArgs.#StatsData")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member", Target = "BNSharp.BattleNet.Product.#UnknownProduct")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "System.Int32.TryParse(System.String,System.Int32@)", Scope = "member", Target = "BNSharp.BattleNet.Stats.StarcraftStats.#.ctor(System.Byte[])")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "System.Int32.TryParse(System.String,System.Int32@)", Scope = "member", Target = "BNSharp.BattleNet.Stats.Warcraft3Stats.#.ctor(System.Byte[])")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "BNSharp.BattleNet.Clans.ClanMemberListEventArgs.#Members")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "BNSharp.Net.BattleNetClient+ParseData.#PacketID")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "BNSharp.Net.BattleNetClient+ParseData.#Length")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "BNSharp.Net.BattleNetClient+ParseData.#Data")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "BNSharp.Net.BattleNetClient+ParseData")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "BNSharp.BattleNet.Friends")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Scope = "member", Target = "BNSharp.Net.BattleNetClient.#RegisterCustomPacketHandler(BNSharp.BncsPacketId,BNSharp.Plugins.ParseCallback,BNSharp.Plugins.ParseCallback&)")]