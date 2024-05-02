import { ResizableElement } from './resizable-element-type.ts'

declare global {
  interface Window {
    animateDialogOpening: (dialogId: string) => Promise<void>
    animateDialogClosing: (dialogId: string) => Promise<void>
    isElementContainsFocusedItem: (element: Element) => boolean
    subscribeElementResize: (element: ResizableElement) => void
    unsubscribeElementResize: (element: ResizableElement) => void
    updateElementSize: (element: Element) => void
  }
}

export {}
