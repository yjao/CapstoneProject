
BANNER = '''
===========================
      GENERAL PARSER
 (Exports from ?SV to ???)

 LATEST UPDATE: 11/22/2015
 PYTHON VERSION: 3.3.4
 AUTHOR: Faye Jao
===========================
'''
print(BANNER)


class FileInfo:
    def __init__(self, _output, _input, delim=None):
        self.src_path = ""
        self.src_file = ""
        self.exp_path = ""
        self.exp_file = ""
        self.src_extn = _input
        self.exp_extn = _output
        self.delimiter = delim
        if _input == "tsv":
            self.delimiter = "\t"
        elif _input == "csv":
            self.delimiter = ","
        if self.delimiter == None:
            print("Please modify the code to include a proper delimiter.")

    def getFullSrcPath(self):
        return self.src_path + self.src_file +"."+ self.src_extn
    def getFullExpPath(self):
        return self.exp_path + self.exp_file +"."+ self.exp_extn


class Program:
    def __init__(self, fileInfo):
        self.allFields = {}
        self.allRowData = []
        self.fileInfo = fileInfo
        self.parseFile()

    def parseFile(self):
        # read the file and split each line into distinct list items
        file = open(self.fileInfo.getFullSrcPath(), 'r')
        lines = []
        for line in file.readlines():
            lines.append(line.split(self.fileInfo.delimiter))
        file.close()
        # pop off the first line for field attribute names
        firstLine = lines.pop(0)
        self.createAllFields(firstLine);
        # for each subsequent row, parse the data
        for line in lines:
            self.insertRowData(line)

    def createAllFields(self, items):
        for i in range(len(items)):
            self.allFields[i] = items[i].strip()

    def insertRowData(self, items):
        rowData = {}
        # find the right field and store with that as the key
        for i in range(len(items)):
            fieldname = self.allFields[i]
            rowData[fieldname] = items[i].strip()
        self.allRowData.append(rowData)

