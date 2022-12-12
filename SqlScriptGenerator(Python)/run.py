from PyConsoleMenu import MultiSelectorMenu
from main import SqlConverter, ActionEnum


def main():
    templates = []
    for action in ActionEnum:
        templates.append(action.name)

    table_name = input('Insert table name: ')
    path = input(f'Insert path (/results/{table_name.upper()}/): ')
    menu = MultiSelectorMenu(templates, title='Select sql template', count=len(templates))
    ans = menu.input()

    for i in ans:
        SqlConverter(i.name, table_name, path).run()

    # SqlConverter(ActionEnum.DROP.name, "access").run()


if __name__ == "__main__":
    main()
