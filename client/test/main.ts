import { app, BrowserWindow } from 'electron';
import * as path from 'path';

let mainWindow: Electron.BrowserWindow | null;

function createWindow() {
    // Create the browser window
    mainWindow = new BrowserWindow({
        height: 600,
        width: 800
    });

    // and load the index.html of the app
    mainWindow.loadFile(path.join(__dirname, './www/index.html'));

    // mainWindow.webContents.openDevTools();

    mainWindow.on('closed', () => {
        // Dispose main window and subwindows
        mainWindow = null;
    });
}

app.on('ready', createWindow);

app.on('window-all-closed', () => {
    // OS X needs to recreate the window, but anothers - dispose
    if(process.platform !== 'darwin') {
        mainWindow = null;
    } 
});

app.on('activate', () => {
    if (mainWindow === null) {
        createWindow();
    }
});