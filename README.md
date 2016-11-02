# VisualGDB Tools

### Overview

**vgdbtools** is a small Visual Studio extension that makes using [VisualGDB](http://visualgdb.com/) easier.

So far only one command exists that allows to change remote connection for all projects in a solution at once.

### Install

* Download `vgdbtools.zip` package from [here](http://cdn.vurdalakov.net/files/vgdbtools/vgdbtools_1_00.zip).
* Unpack it to any location.
* Double-click the `vgdbtools.vsix` file and follow instructions.

### Use

* In `Tools` menu, open `VisualGDB Tools` submenu and click `Change Remote Connection...` menu item.
* Select required connection from the combobox and click `OK`.

![VisualGDB Tools, Change Remote Connection command](https://raw.githubusercontent.com/vurdalakov/vgdbtools/master/img/screenshot1.png)
![VisualGDB Tools, Change Remote Connection command](https://raw.githubusercontent.com/vurdalakov/vgdbtools/master/img/screenshot2.png)

### F.A.Q.

###### How to add a new remote connection?

* Add a new remote connection in any project using ``
* Use `Change Remote Connection...` command of `VisualGDB Tools` to change all other projects to the new connection.

### History

###### Version 1.00 (02.11.2016)

* First public version.

### License

Distributed under the terms of the [MIT license](https://opensource.org/licenses/MIT).

### Disclaimer

This project is not associated with nor sponsored by [VisualGDB](http://visualgdb.com/) nor [Sysprogs OÜ](http://sysprogs.com/).

```
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
```
