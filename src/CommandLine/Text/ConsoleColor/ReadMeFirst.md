
ColorConsole is build to support color help without BREAK CHANGE on the binary level.
The de-facto standard ansi color codes is used.
 current applications are still working without change using the new colorconsole feature.

Style is disabled by default.
Stylebuilder is initialised in Parser class by setting the ParerSetting corresponding property.
Ansi Color is the default in Linux and OS x.

StyleBuilder and ColorBuilder are factory classes.

In Test, TestsFixture.cs is addeded to reset the static properties in Stylebuilder/colorbuilder between test sessions.

A demo application is availabel to try the feature.

