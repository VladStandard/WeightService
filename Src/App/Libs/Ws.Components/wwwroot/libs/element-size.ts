import { ResizableElement } from './types/resizable-element-type.ts'

/**
 * Subscribes to element resize events and updates the element's dropdown width based on the button width.
 *
 * @param {ResizableElement} element - The element to subscribe to resize events for.
 * @return {void} This function does not return anything.
 */
window.subscribeElementResize = (element: ResizableElement): void => {
  if (!element) return
  const resizeHandler = () => resizeElement(element)
  resizeHandler()
  window.addEventListener('resize', resizeHandler)
  element.resizeHandler = resizeHandler
}

/**
 * Unsubscribes from element resize events and clears the resize handler for the given element.
 *
 * @param {ResizableElement} element - The element to unsubscribe from resize events for.
 * @return {void} This function does not return anything.
 */
window.unsubscribeElementResize = (element: ResizableElement): void => {
  const resizeHandler = element.resizeHandler
  if (!resizeHandler) return
  window.removeEventListener('resize', resizeHandler)
  delete element.resizeHandler
}

/**
 * Updates the size of the element by resizing it using the `resizeElement` function.
 *
 * @param {Element} element - The element to update the size for.
 * @return {void} This function does not return anything.
 */
window.updateElementSize = (element: Element): void => resizeElement(element)

const resizeElement = (element: Element) => {
  const button = element.querySelector('.select-button') as HTMLElement | null
  const dropdown = element.querySelector('.select-dropdown') as HTMLElement | null
  if (dropdown && button) dropdown.style.width = `${button.offsetWidth}px`
}
