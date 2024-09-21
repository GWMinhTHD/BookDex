import React, { useEffect, useState } from "react";
import BookStoreApi from "../../api/BookStoreApi";
import { Worker, Viewer } from "@react-pdf-viewer/core";
import { toolbarPlugin, ToolbarSlot, TransformToolbarSlot } from '@react-pdf-viewer/toolbar'
import '@react-pdf-viewer/core/lib/styles/index.css'
import '@react-pdf-viewer/toolbar/lib/styles/index.css'
import { useParams } from "react-router-dom";


function ReaderPage() {
  const [book, setBook] = useState(null);
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

  useEffect(() => {
    BookStoreApi.getUserBook(id).then((item) => {
      setBook(item.data);
    });
  }, []);

  return (
    <>
      {book ? (
        <div className="min-h-screen bg-gray-100 flex items-center justify-center p-4 sm:p-6 md:p-8">
          <div className="w-full max-w-6xl h-[85vh] sm:h-[90vh] bg-white shadow-lg rounded-lg overflow-hidden">
            <Worker workerUrl="https://unpkg.com/pdfjs-dist@3.4.120/build/pdf.worker.min.js">
              <div className="h-full">
                <div
                  style={{
                    alignItems: "center",
                    backgroundColor: "#eeeeee",
                    borderBottom: "1px solid rgba(0, 0, 0, 0.1)",
                    display: "flex",
                    padding: "4px",
                  }}
                >
                  <Toolbar>{renderDefaultToolbar(transform)}</Toolbar>
                </div>
                <Viewer
                  fileUrl={`https://localhost:7216/pdf/${book.fileLocation}`}
                  plugins={[toolbarPluginInstance]}
                  defaultScale={1.0}
                  renderLoader={(percentages) => (
                    <div className="flex items-center justify-center h-full">
                      <p className="text-gray-500">
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