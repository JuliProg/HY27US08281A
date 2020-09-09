using NAND_Prog;
using System;
using System.ComponentModel.Composition;

namespace HY27US08281A
{
    /*
     use the design :

      # region
         <some code>
      # endregion

    for automatically include <some code> in the READMY.md file in the repository
    */

    #region
    public class ChipAssembly
    {
        [Export("Chip")]
        ChipPrototype myChip = new ChipPrototype();
        #endregion


        #region Chip parameters

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

        #endregion


            #region Chip operations

            //------- Add chip operations ----------------------------------------------------

            myChip.Operations("Reset_FFh").
                   Operations("Erase_60h_D0h").
                   Operations("Read_00h").
                   Operations("PageProgram_80h_10h");

            #endregion



            #region Chip registers (optional)

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
                

            #endregion


        }
      
    }

}
