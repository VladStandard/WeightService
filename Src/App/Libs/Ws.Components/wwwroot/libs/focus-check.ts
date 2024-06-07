/**
 * Check if the given element contains the currently focused element.
 *
 * @param {Element} element - The element to check.
 * @return {boolean} Whether the element contains the focused element.
 */
window.isElementContainsFocusedItem = (element: Element): boolean => {
  if (!element) return false
  return element.contains(document.activeElement)
}
