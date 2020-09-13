![Create new chip](https://github.com/JuliProg/HY27US08281A/workflows/Create%20new%20chip/badge.svg?event=repository_dispatch)
![ChipUpdate](https://github.com/JuliProg/HY27US08281A/workflows/ChipUpdate/badge.svg)
# Join the development of the project ([list of tasks](https://github.com/users/JuliProg/projects/1))


# HY27US08281A
Implementation of the HY27US08281A chip for the JuliProg programmer

Dependency injection, DI based on MEF framework is used to connect the chip to the programmer.

<section class = "listing">

#
```c#

    public class ChipAssembly
    {
        [Export("Chip")]
        ChipPrototype myChip = new ChipPrototype();
```
# Chip parameters
```c#


        ChipAssembly()
        {
            myChip.devManuf = "Hynix";
            myChip.name = "HY27US08281A";
            myChip.chipID = "AD73";            // device ID - ADh 73h (HY27US08281A.pdf page 30)

            myChip.width = Organization.x8;    // chip width - 8 bit
            myChip.bytesPP = 512;             // page size - 512 byte
            myChip.spareBytesPP = 16;          // size Spare Area - 16 byte
            myChip.pagesPB = 32;               // the number of pages per block - 32 
            myChip.bloksPLUN = 1024;           // number of blocks in CE - 1024
            myChip.LUNs = 1;                   // the amount of CE in the chip
            myChip.colAdrCycles = 1;           // cycles for column addressing
            myChip.rowAdrCycles = 2;           // cycles for row addressing 
            myChip.vcc = Vcc.v3_3;             // supply voltage

```
# Chip operations
```c#


            //------- Add chip operations ----------------------------------------------------

            myChip.Operations("Reset_FFh").
                   Operations("Erase_60h_D0h").
                   Operations("Read_00h").
                   Operations("PageProgram_80h_10h");

```
# Chip registers (optional)
```c#


            //------- Add chip registers (optional)----------------------------------------------------

            myChip.registers.Add(
                "Status Register").
                Size(1).
                Operations("ReadStatus_70h").
                Interpretation("SR_Interpreted").   //From https://github.com/JuliProg/Wiki/wiki/Status-Register-Interpretation
                UseAsStatusRegister();



            myChip.registers.Add(
                "Id Register").
                Size(2).
                Operations("ReadId_90h");               
                

```
</section>








footer
