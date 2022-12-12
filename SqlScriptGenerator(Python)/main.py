from enum import Enum, auto
from os import makedirs


class ActionEnum(Enum):
    DROP = auto()
    INSERT = auto()
    SELECT = auto()
    STRUCTURE = auto()
    STRUCTURE_FK = auto()
    TRUNC = auto()


class SqlConverter:

    def __init__(self, action: str, table_name: str, path: str):
        self.__action = action.lower()
        self.__table_name = table_name.upper()
        if not path:
            self.__path = f'results/{self.__table_name}'
        else:
            self.__path = f'{path}/{self.__table_name}'

    def __parse_file(self) -> str:
        sql_script = ''
        templates_editor = {
            '{table_name}': self.__table_name,
        }

        with open(f"sql_templates/{self.__action}.sql", 'r') as my_file:
            sql_script = my_file.read()

        for key, value in templates_editor.items():
            if sql_script.find(key):
                sql_script = sql_script.replace(key, value)

        return sql_script

    def __save_file(self, info: str):
        makedirs(self.__path, exist_ok=True)
        file_name = f'{self.__table_name} {self.__action.upper()}.sql'
        with open(f"{self.__path}/{file_name}", 'w+') as new_file:
            new_file.write(info)
            print(f'FILE {file_name} was created at: {self.__path}/')

    def run(self):
        file_info = self.__parse_file()
        self.__save_file(file_info)


