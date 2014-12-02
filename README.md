Crypto Brute Force
=========
####Overview of Crypto Brute Force
- Using [brute-force search / exhaustive] to generate and find checksums (Generate and test).
- Customise search method / efficiency.
- Create brute-force work and run at a later time.
- Run brute force attack with multiple computers over TCP/IP.
 
####To do List - To contribute simple attempt the to do list
- Complete enable and disable of form controls to prevent users from selecting unavailable options
- comprehensive test
 
Images
----
![UI tag](https://lh5.googleusercontent.com/-GMg4EtvaBQA/VAxlX_WVdgI/AAAAAAAAAII/Rb1V_gTzjvg/s400/databruteforce.png)

Version
----
beta - Join and report bugs

How to use - Offline Mode
--------------
- Build project using visual studios or
- Simply run oCryptoBruteForce.exe with the dlls in the same directory (oCrypto.dll and PortableLib.dll)
- Select File and Open the file you want to get the checksum of.
- Select if you want to use exhaustive search and if base64 string is detected, you will also have the option to select that.
- Select the checksum type
- Press the Add work button
- Select work and right click on it, followed by pressing start to begin brute force attack.
- If the brute force is successful note down the checksum offset and generation length

Note
--------------
> The application is currently still in development phase.

License
----
[GPLv3] - General Public License

[GPLv3]:http://www.gnu.org/licenses/gpl-3.0-standalone.html
[brute-force search / exhaustive]:http://en.wikipedia.org/wiki/Brute-force_search