import React, { useEffect, useState } from "react";
import BookStoreApi from "../../api/BookStoreApi";
import { Worker, Viewer } from "@react-pdf-viewer/core";
import { toolbarPlugin, ToolbarSlot, TransformToolbarSlot } from '@react-pdf-viewer/toolbar'
import '@react-pdf-viewer/core/lib/styles/index.css'
import '@react-pdf-viewer/toolbar/lib/styles/index.css'
import { useParams } from "react-router-dom";
import "../../pages/ReaderPage/reader.css";


function ReaderPage() {
  const [book, setBook] = useState(null);
  const [url, setURL] = useState();
  const [initialPage, setInitialPage]= useState(0)
  const { id } = useParams();

const toolbarPluginInstance = toolbarPlugin();
const { renderDefaultToolbar, Toolbar } = toolbarPluginInstance;

const transform = (slot) => ({
  ...slot,
  // These slots will be empty
  Download: () => <></>,
  DownloadMenuItem: () => <></>,
  SwitchTheme: () => <></>,
  SwitchThemeMenuItem: () => <></>,
  Open: () => <></>,
  OpenMenuItem: () => <></>,
  ShowProperties: () => <></>,
  ShowPropertiesMenuItem: () => <></>,
  Print: () => <></>,
  PrintMenuItem: () => <></>,
  SwitchScrollMode: () => <></>,
  SwitchScrollModeMenuItem: () => <></>,
  SwitchViewMode: () => <></>,
  SwitchViewModeMenuItem: () => <></>,
  //SwitchSelectionMode: () => <></>,
  //SwitchSelectionModeMenuItem: () => <></>,
  });

  const handlePageChange = (e) => {
    BookStoreApi.updatePage(book.bookId, e.currentPage);
  };

  useEffect(() => {
    BookStoreApi.getUserBook(id).then((item) => {
      setBook(item.data);
      setInitialPage(item.data.pageNum);
      const blob = base64toBlob(item.data.fileLocation);
      setURL(URL.createObjectURL(blob));
    });
  }, []);

  const base64toBlob = (data) => {
    const bytes = atob(data);
    let length = bytes.length;
    let out = new Uint8Array(length);

    while (length--) {
        out[length] = bytes.charCodeAt(length);
    }

    return new Blob([out], { type: 'application/pdf' });
};


  return (
    <>
      {book ? (
        <div className="min-h-screen bg-gray-900 flex items-center justify-center p-4 sm:p-6 md:p-8">
          <div className="w-full max-w-6xl h-[85vh] sm:h-[90vh] bg-gray-800 shadow-lg rounded-lg overflow-hidden">
            <Worker workerUrl="https://unpkg.com/pdfjs-dist@3.4.120/build/pdf.worker.min.js">
              <div className="h-full">
                <div
                  style={{
                    alignItems: "center",
                    backgroundColor: "#1f2937",
                    borderBottom: "1px solid rgba(255, 255, 255, 0.1)",
                    display: "flex",
                    padding: "4px",
                  }}
                >
                  <Toolbar>{renderDefaultToolbar(transform)}</Toolbar>
                </div>
                <Viewer
                  fileUrl={url}
                  initialPage={initialPage}
                  onPageChange={handlePageChange}
                  plugins={[toolbarPluginInstance]}
                  defaultScale={1.0}
                  theme="dark"
                  renderLoader={(percentages) => (
                    <div className="flex items-center justify-center h-full bg-gray-800 text-gray-200">
                      <p>
                        Loading... {Math.round(percentages)}%
                      </p>
                    </div>
                  )}
                />
              </div>
            </Worker>
          </div>
        </div>
      ) : (
        <></>
      )}
    </>
  );
}

export default ReaderPage;