"use strict";
var __importStar = (this && this.__importStar) || function (mod) {
    if (mod && mod.__esModule) return mod;
    var result = {};
    if (mod != null) for (var k in mod) if (Object.hasOwnProperty.call(mod, k)) result[k] = mod[k];
    result["default"] = mod;
    return result;
};
Object.defineProperty(exports, "__esModule", { value: true });
var electron_1 = require("electron");
var path = __importStar(require("path"));
var mainWindow;
function createWindow() {
    // Create the browser window
    mainWindow = new electron_1.BrowserWindow({
        height: 600,
        width: 800
    });
    // and load the index.html of the app
    mainWindow.loadFile(path.join(__dirname, './www/index.html'));
    // mainWindow.webContents.openDevTools();
    mainWindow.on('closed', function () {
        // Dispose main window and subwindows
        mainWindow = null;
    });
}
electron_1.app.on('ready', createWindow);
electron_1.app.on('window-all-closed', function () {
    // OS X needs to recreate the window, but anothers - dispose
    if (process.platform !== 'darwin') {
        mainWindow = null;
    }
});
electron_1.app.on('activate', function () {
    if (mainWindow === null) {
        createWindow();
    }
});
//# sourceMappingURL=main.js.map