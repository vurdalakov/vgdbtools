# VisualGDB Tools
zz
### Overview

**VisualGDB Tools** is a small Visual Studio extension that makes using [VisualGDB](http://visualgdb.com/) easier.

So far only one command exists that allows to change remote connection for all projects in a solution at once.

Supported Visual Studio 2015 Editions:
* Visual Studio 2015
* Visual Studio 2015 Community

### Install

##### Install from the Visual Studio Gallery

1. In Visual Studio, on the `Tools menu`, click `Extensions and Updates`.
1. Click `Online` and then search for `VisualGDB Tools`. The listing for the `VisualGDB Tools` extension should appear.
1. Click `Download`. After the extension is downloaded, click `Install`.
1. To complete the installation, restart Visual Studio.

##### Install manually

1. Download `vgdbtools.zip` package from [here](http://cdn.vurdalakov.net/files/vgdbtools/vgdbtools_1_00.zip).
1. Unpack it to any location.
1. Double-click the `vgdbtools.vsix` file and follow instructions.
1. To complete the installation, restart Visual Studio.

### Usage

1. In `Tools` menu, open `VisualGDB Tools` submenu and click `Change Remote Connection...` menu item.
1. Select required connection from the combobox and click `OK`.

![VisualGDB Tools, Change Remote Connection command](https://raw.githubusercontent.com/vurdalakov/vgdbtools/master/img/screenshot3.png)

![VisualGDB Tools, Change Remote Connection command](https://raw.githubusercontent.com/vurdalakov/vgdbtools/master/img/screenshot1.png)
![VisualGDB Tools, Change Remote Connection command](https://raw.githubusercontent.com/vurdalakov/vgdbtools/master/img/screenshot2.png)

### F.A.Q.

###### How to add a new remote connection?

1. Add a new remote connection in any project using `VisualGDB Project Properties` dialog.
1. Use `Change Remote Connection...` command of `VisualGDB Tools` to change all other projects to the new connection.

### History

###### Version 1.00 (02.11.2016)

* First public version.

### License

Distributed under the terms of the [MIT license](https://opensource.org/licenses/MIT).

### Disclaimer

This project is not associated with nor sponsored by [VisualGDB](http://visualgdb.com/) nor [Sysprogs OÜ](http://sysprogs.com/).

All product names, logos, and brands are property of their respective owners and are cited herein for identification purposes only.

```
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
```
