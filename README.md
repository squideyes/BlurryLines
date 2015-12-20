# BlurryLines

The BlurryLines WPF application has a User Control ("BlurryLines.Board") that renders a grid full of blurry lines.  The lines should be exactly 1-pixel wide, unaliased black and sharp, but I haven't been able to figure out how to make this happen.  Note: The bluriness varies depending up the position of the control as well as the specified number of Rows and/or Columns and the CellSize setting. 

I've found a number of articles on the net that explain what's probably going on (i.e. http://wpftutorial.net/DrawOnPhysicalDevicePixels.html) but I haven't been able to get things to work as I hoped.  Hopefully you'll have better luck.
