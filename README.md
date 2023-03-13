# MultiDelete
MultiDelete is a GUI-Based Application to delete your Minecraft worlds and more. It is fast, has a lot of different options and is very easy to install and use.

![deletingWorlds](https://user-images.githubusercontent.com/107059342/205977255-5fe9b666-8dce-4e0e-a4b4-6378a50e425c.png)

## Settings
![Instances](https://user-images.githubusercontent.com/107059342/209198899-ae8c426a-7538-4da7-b399-cb552d29e0bc.png)  
![Criteria](https://user-images.githubusercontent.com/107059342/209198916-5fdbd732-cca0-4053-8cbc-c26459ef8059.png)  
![OtherFiles](https://user-images.githubusercontent.com/107059342/209198934-c3ae5f33-0582-4075-b906-8750d556bcf1.png)  
![Advanced](https://user-images.githubusercontent.com/107059342/209198946-433f5543-71ac-4ac4-ae5f-f2733e1e4fdf.png)  
![Appearance](https://user-images.githubusercontent.com/107059342/209198951-38d70f05-0aa9-4300-8d8e-f9aca4721a5b.png)  

## Launch Arguments
`-delWorlds` This starts world deletion on startup  
`-closeAfterDeletion` This makes the program close after world deletion is finished  
`-dontCheckUpdates` This makes it so MultiDelete doesnt check for updates upon startup

## How to use MultiDelete with [MultiResetWall](https://github.com/Specnr/MultiResetWall)
Open the scripts folder in your wall folder and edit functions.ahk. Replace the WorldBop function at line 773 with this:  

    WorldBop() {
      Run "C:\Program Files (x86)\MultiDelete\MultiDelete.exe" -delWorlds -closeAfterDeletion -dontCheckUpdates
    }  
You may need to change the path of the program if yours is different and you may modify the launch arguments to your needs. You can find a list of all launch args [here](https://github.com/greyhayv/MultiDelete#launch-arguments). Now when you click the Delete Worlds option in the tray it will delete your worlds using MultiDelete.

## Installation
Click on the latest release, download the installer, execute it and go through the steps.

## Contact
If you have any Issues, questions, suggestions or have found any bugs feel free to dm me on discord: greyhayv#4237
