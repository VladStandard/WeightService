import { DotNetObjectType } from './dotnet-object-type.ts'
import { ResizableElement } from './resizable-element-type.ts'

declare global {
  interface Window {
    animateDialogOpening: (dialogId: string) => Promise<void>
    animateDialogClosing: (dialogId: string) => Promise<void>
    isElementContainsFocusedItem: (element: Element) => boolean
    subscribeElementResize: (element: ResizableElement) => void
    unsubscribeElementResize: (element: ResizableElement) => void
    updateElementSize: (element: Element) => void
    switchTheme: (theme: string) => void
    initializeTheme: () => void
    subscribeBarcodeEnterEvent: (dotNetObjRef: DotNetObjectType, functionName: string) => void
    unsubscribeBarcodeEnterEvent: () => void
    subscribeMiddleMouseClickEvent: (dotNetObjReference: DotNetObjectType, functionName: string) => void
    unsubscribeMiddleMouseClickEvent: () => void
  }
}

export {}
