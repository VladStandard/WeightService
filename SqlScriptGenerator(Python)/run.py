from PyConsoleMenu import MultiSelectorMenu
from main import SqlConverter, ActionEnum


def main():
    templates = [action.name for action in ActionEnum]

    table_name = input('Insert table name: ').upper()
    path = input(f'Insert path (/results/{table_name}/): ')

    menu = MultiSelectorMenu(templates, title='Select sql template')
    ans = menu.input()
    
    for i in ans:
        SqlConverter(i.name, table_name, path).run()


if __name__ == "__main__":
    main()
