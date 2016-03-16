MBNCSUtil is a lightweight Battle.net library used for developing client applications by implementing the authentication and versioning protocols.  It works well for people who want to implement the client connection by themselves, but who don't want the fuss of having to reimplement all of the complex math and code for authenticating their client.

BN# (BN-Sharp) takes it a step further and implements a fully-featured, event-based API for creating client connections to Battle.net.  The connection is abstracted away from the developer, allowing the developer to focus on rich features.  BN# is highly optimized and flexible; incoming packets can be prioritized, events can be prioritized -- or you can ignore that entirely.

JinxBot is a synthesis of BN# - it is a plugin-oriented Battle.net client that can emulate Starcraft, Diablo II, Warcraft II: Battle.net Edition, and Warcraft III.  It provides a rich user interface and optional features such as clan management and moderation.

JinxBot.Controls is a separate library used by JinxBot to facilitate client development in Windows Forms.  It implements controls such as docking, tabbed interfaces, and chat windows.  The chat window interface is based in HTML and is extensible with some simple class implementations, so you can come up with new controls on your own.

JinxBot[Web](Web.md) is a plugin for JinxBot and a web application that uses ASP.NET AJAX and WCF to bubble events from JinxBot to a web site that then allows the channel JinxBot is currently residing in to be viewed from a browser.

JinxBot[Web](Web.md) will stay as part of this repository, but you can find additional plugins at http://jinxbot-plugins.googlecode.com/.

&lt;wiki:gadget url="http://www.ohloh.net/p/20669/widgets/project\_cocomo.xml" height="240" border="0"/&gt;