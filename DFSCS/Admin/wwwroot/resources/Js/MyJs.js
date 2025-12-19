
function initializeQuill(editorId, initialContent) {
    var quill = new Quill(`#${editorId}`, {
        theme: 'snow',
        modules: {
            toolbar: [
                ['bold', 'italic', 'underline', 'strike'],        // Text styling
                ['blockquote', 'code-block'],                     // Block-level elements

                [{ 'header': 1 }, { 'header': 2 }],               // Header styles
                [{ 'list': 'ordered' }, { 'list': 'bullet' }],    // Lists
                [{ 'script': 'sub' }, { 'script': 'super' }],     // Subscript/Superscript
                [{ 'indent': '-1' }, { 'indent': '+1' }],         // Indent
                [{ 'direction': 'rtl' }],                         // Text direction

                [{ 'size': ['small', false, 'large', 'huge'] }],  // Font sizes
                [{ 'header': [1, 2, 3, 4, 5, 6, false] }],        // Header levels

                [{ 'color': [] }, { 'background': [] }],          // Colors
                [{ 'font': [] }],                                 // Font types
                [{ 'align': [] }],                                // Text alignment

                ['link', 'image', 'video'],                       // Media
                ['clean'],                                       // Clear formatting
                ['table'],                                        // Table control
            ],
            table: true,
        }
    });
    
    // Set initial content
    quill.root.innerHTML = initialContent || '';
}

function getQuillContent(editorId) {
    var editorElement = document.getElementById(editorId);
    if (!editorElement) {
        console.error(`Editor element with ID ${editorId} not found.`);
        return "";
    }
    var quill = Quill.find(editorElement);

    if (!quill) {
        console.error(`Quill editor instance not found for element with ID ${editorId}`);
        return "";
    }
    return quill.root.innerHTML;
    //var quill = Quill.find(`#${editorId}`);
    //return quill.root.innerHTML;
}
function disableRightClickAndRemoveDownload() {
    // Select all video elements on the page
    const videoElements = document.querySelectorAll('video');
    // Loop through each video element and attach an event listener to disable right-click
    videoElements.forEach(video => {
        video.addEventListener('contextmenu', function (e) {
            e.preventDefault(); // This will disable the right-click menu entirely for video elements
        });
        video.setAttribute("controlslist", "nodownload");
    });
}

function AddTable() {
    // Ensure that Quill is loaded
    const quill = document.querySelector('.ql-editor');
    if (quill) {
        // Register quill-better-table
        const BetterTable = window.QuillBetterTable.default;
        if (BetterTable) {
            quill.quill.addModule('better-table', BetterTable);
        }
    }
}

document.addEventListener('DOMContentLoaded', function () {
    // Select all input elements
    var inputs = document.querySelectorAll('h1');

    // Loop through each input and remove the 'tabindex' attribute
    inputs.forEach(function (h1) {
        h1.removeAttribute('tabindex');
    });
});

//window.addEventListener("offline", () => {
//    document.getElementById("components-reconnect-modal").style.display = "none";
//});

window.getScrollTop = (element) => element.scrollTop;
window.getScrollHeight = (element) => element.scrollHeight;
window.getClientHeight = (element) => element.clientHeight;


    window.downloadBase64Pdf = function (base64, filename) {
        const byteCharacters = atob(base64);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
    const byteArray = new Uint8Array(byteNumbers);
    const blob = new Blob([byteArray], {type: "application/pdf" });

    const link = document.createElement("a");
    link.href = URL.createObjectURL(blob);
    link.download = filename;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    };

window.downloadFileFromBase64 = (filename, mimeType, base64) => {
    const link = document.createElement('a');
    link.href = "data:" + mimeType + ";base64," + base64;
    link.download = filename;
    link.click();
};

window.startQRScanner = async function () {
    try {
        const constraints = { video: { facingMode: "environment" } };
        const stream = await navigator.mediaDevices.getUserMedia(constraints);

        const video = document.getElementById("qr-video");
        video.style.display = "block";
        video.srcObject = stream;

        const scanner = new ZXing.BrowserQRCodeReader();
        scanner.decodeFromVideoDevice(null, "qr-video", (result, err) => {
            if (result) {
                // Stop stream after successful scan
                stream.getTracks().forEach(track => track.stop());
                video.style.display = "none";
                DotNet.invokeMethodAsync('SetScannedValue', result.text);
                scanner.reset();
            }
        });
    } catch (err) {
        alert("Camera access denied or not available.");
        console.error(err);
    }
}