export const getIsNestedItem = (element) => {
  if (!element) return false
  return element.contains(document.activeElement);
}