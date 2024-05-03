export type DotNetObjectType = {
  invokeMethodAsync<T>(methodIdentifier: string, ...args: unknown[]): Promise<T>
}
