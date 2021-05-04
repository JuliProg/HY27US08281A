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

    public class ChipPrototype_v1 : ChipPrototype
    {
        public int EccBits;
    }


    public class ChipAssembly
    {
        [Export("Chip")]
        ChipPrototype myChip = new ChipPrototype_v1();
        


        #region Chip parameters

        ChipAssembly()
        {
            //--------------------Vendor Specific Pin configuration---------------------------

            //  VSP1(38pin) - GND    
            //  VSP2(35pin) - NC
            //  VSP3(20pin) - NC

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
            (myChip as ChipPrototype_v1).EccBits = 1;                // required Ecc bits for each 512 bytes

            #endregion


            #region Chip operations

            //------- Add chip operations   https://github.com/JuliProg/Wiki#command-set----------------------------------------------------

            myChip.Operations("Reset_FFh").
                   Operations("Erase_60h_D0h").
                   Operations("Read_00h").
                   Operations("PageProgram_80h_10h");

            #endregion



            #region Chip registers (optional)

            //------- Add chip registers (optional)----------------------------------------------------

            myChip.registers.Add(                   // https://github.com/JuliProg/Wiki/wiki/StatusRegister
                "Status Register").
                Size(1).
                Operations("ReadStatus_70h").
                Interpretation("SR_Interpreted").
                UseAsStatusRegister();



            myChip.registers.Add(                  // https://github.com/JuliProg/Wiki/wiki/ID-Register
                "Id Register").
                Size(2).
                Operations("ReadId_90h");               
                

            #endregion


        }
      
    }

}
