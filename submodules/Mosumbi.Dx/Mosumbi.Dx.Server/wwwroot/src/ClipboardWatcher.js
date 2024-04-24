import { ViewerApp } from "./App.js";
import { ShowToast } from "./UI.js";
export class ClipboardWatcher {
    WatchClipboard() {
        var _a;
        if (!location.protocol.includes("https") &&
            !location.origin.includes("localhost")) {
            console.warn("Clipboard API only works in a secure context (i.e. HTTPS or localhost).");
            return;
        }
        if (!((_a = navigator.clipboard) === null || _a === void 0 ? void 0 : _a.readText)) {
            console.warn("Clipboard API not supported.");
            return;
        }
        if (this.ClipboardTimer) {
            console.log("ClipboardWatcher is already running.");
            return;
        }
        this.ClipboardTimer = setInterval(() => {
            if (!document.hasFocus()) {
                return;
            }
            if (this.NewClipboardText && navigator.clipboard.writeText) {
                navigator.clipboard.writeText(this.NewClipboardText);
                this.LastClipboardText = this.NewClipboardText;
                this.NewClipboardText = null;
                ShowToast("Clipboard updated.");
                return;
            }
            navigator.clipboard.readText().then(newText => {
                if (this.LastClipboardText != newText) {
                    this.LastClipboardText = newText;
                    ViewerApp.MessageSender.SendClipboardTransfer(newText, false);
                }
            });
        }, 500);
    }
    SetClipboardText(text) {
        if (text == this.LastClipboardText) {
            return;
        }
        this.NewClipboardText = text;
    }
}
//# sourceMappingURL=ClipboardWatcher.js.map