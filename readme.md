# Traffic Light Control 

## Description
A little Windows system tray icon that lets the user select to set a traffic ligt to green, yellow, or red.  Uses X-10 to control the devices.  Each light (red,green,yellow) should be connected to a separate X-10 lamp control.

This project came about as a need to inform my kids when they could come into "Daddy's Office".  I work from home in a room with glass doors so they needed to know when it was okay to come in or not.  I bought my [traffic light off ebay](http://www.ebay.com/sch/i.html?_nkw=traffic+light&_armrs=1&_from=&_ipg=).

* Red = daddy's focusing, don't go in
* Yellow = it's okay to give daddy a quick hug
* Green = daddy's off work, play time!

I've also used it in the past to automate indication of build status, green = build is good, yellow = building, red = build break.  The uses are endless.

## Setup
1. Get yourself a set of three lights, like a [traffic light](http://www.ebay.com/sch/i.html?_nkw=traffic+light&_armrs=1&_from=&_ipg=) or [lamp post](http://amzn.com/B001ANRC3E/?tag=azdp-20) w [colored bulbs](http://www.amazon.com/s?tag=azdp-20&url=index%3Dblended&keywords=colored+light+bulbs)
2. Wire it up for X-10 home automation control.  One [X-10 controller](http://www.ebay.com/sch/i.html?_nkw=LM465&_sacat=0&_odkw=CM17A&_osacat=0&_armrs=1) per light (or [bulb screw](http://www.ebay.com/sch/i.html?_nkw=LM15A-C&_armrs=1&_from=&_ipg=) controller).
3. You'll need a way to get an X-10 signal from the PC onto the powerline.  Get a [wireless receiver](http://www.ebay.com/sch/i.html?_nkw=TM751&_armrs=1&_from=&_ipg=) and [Firecracker transmitting](http://www.ebay.com/sch/i.html?_nkw=CM17A&_sacat=0&_odkw=Firecracker+x10&_osacat=0&_armrs=1) module, you'll probably need a [USB to RS232 cable](http://amzn.com/B0007T27H8/?tag=azdp-20) as well.  I'd recommend a [manual controller](http://www.ebay.com/sch/i.html?_nkw=KR22A&_sacat=0&_odkw=x10+controller&_osacat=0&_armrs=1) for testing.  This [USB unit](http://amzn.com/B0027RMIAO/?tag=azdp-20) may work, but haven't tested it myself.
3. Get this app
4. Run once, close, then edit the user settings .config file to set your X-10 comport, house code, and unit codes for each light
5. Put a shortcut to the app in Start Menu > All Programs > Startup folder if ya want it always running

## Disclaimer
This was done totally for entertainment in just an couple hours.  Don't use it for any actual traffic. :)