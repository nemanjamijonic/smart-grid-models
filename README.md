# Smart Grid Modeling Project

## Introduction

This project focuses on the integration of models according to the IEC CIM standard, utilizing XMI files generated in the _Enterprise Architect_ tool. Our goal is to generate diagrams and models from these XMI files in various formats, such as RDFS, OWL, and HTML. Afterward, we create XML files that can be generated either manually or automatically using the _CIMMET_ tool.

## Process Flow

1. **Receiving the XMI File**  
   At the beginning of the process, we receive the XMI file (in this case _IES2023.xmi_) generated in the _Enterprise Architect_ tool. This file contains a CIM model in accordance with the IEC CIM standard. The XMI format allows the structure of the model to be transferred between tools and used in the next steps.

2. **Creating Diagrams Based on a Schema**  
   Our next step is to create diagrams using the XMI file, based on a predefined schema. The schema determines which classes and attributes we take from the XMI file when generating our models. This schema is crucial as it enables us to accurately extract the necessary data from large models.

3. **Saving Diagrams in Different Formats**  
   After generating the diagrams, we save them in several formats for easier processing and further use:

   - **RDFS** (Resource Description Framework Schema): Used for structured data representation and exchange.
   - **OWL** (Web Ontology Language): Used to represent ontologies, enabling semantic modeling.
   - **HTML**: This format allows for easy viewing of diagrams in web browsers, providing interactivity and a better user experience.

4. **Creating XML Files**  
   After creating and saving the diagrams, the next step is to generate XML files. These files contain information about the models and are used for data exchange between different systems. There are two ways to generate XML:

   - **Manual Creation**: XML files can be manually created if necessary.
   - **Generation via the CIMMET Tool**: We often use the _CIMMET_ tool, which automatically generates XML files based on the created diagrams.

   **NOTE**: We can only create objects for **concrete classes**. Concrete classes are those that are not inherited by any other class. They can be referenced in other classes but cannot be inherited.

5. **DLL from CIM Profile**  
   The _CIMProfileCreator_ project generates a `.dll` file based on our CIM profile in the RDFS format. Later in the process, this `.dll` file will be used to map the objects created in step 4 with the concrete model defined by the `.dll`.

6. **CIMAdapter**  
   The _CIMAdapter_ has two main functionalities:
   - Importing concrete objects from the `.dll` file.
   - Converting them to C# classes for use in our code.

## Defining Description

To successfully map objects from the `.dll` to C# classes, we must define:

- **DMS Types**: These apply only to **concrete classes**, following industry rules.
- **Model Codes**: All classes must have model codes.  
  We also need to define enumerations and classes with properties that match the classes from the RDFS file (steps 2 and 3).

## Testing Results

Project testing can be done using the following methods:

1. **Get Values**
2. **Get Related Values**
3. **Get Extent Values**

---

### Method Details

#### 1. `GetValues(long globalId)`

- **Parameter:**

  - `globalId` (type: `long`): A unique identifier representing the resource whose values are being retrieved.

- **Functionality:**
  This method retrieves all properties of a resource specified by the `globalId`. It:
  - Extracts the type of the resource using `ModelCodeHelper.ExtractTypeFromGlobalId`.
  - Fetches available property IDs for the resource type using `modelResourcesDesc.GetAllPropertyIds()`.
  - Queries the GDA service to retrieve values and exports the result to an XML file.

#### 2. `GetExtentValues(ModelCode modelCode)`

- **Parameter:**

  - `modelCode` (type: `ModelCode`): Specifies the type of resources to retrieve.

- **Functionality:**
  This method retrieves the values for all instances of a given resource type (`modelCode`). It fetches resources in batches using an iterator from the GDA service, storing their IDs and exporting details to an XML file.

#### 3. `GetRelatedValues(long sourceGlobalId, Association association)`

- **Parameters:**

  - `sourceGlobalId` (type: `long`): The identifier of the source resource.
  - `association` (type: `Association`): Specifies the relationship based on which related resources are retrieved.

- **Functionality:**
  This method retrieves the related resources for the specified `sourceGlobalId` based on the `association`. The related resource values are fetched in batches and exported to an XML file.

---
